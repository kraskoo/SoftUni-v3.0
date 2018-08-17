$(() => {
	const app = Sammy('#container', function() {
		this.use('Handlebars', 'hbs');
		this.get('index.html', getWelcomePage);
		this.get('/', getWelcomePage);
		this.get('#/home', getWelcomePage);
		this.post('#/register', (ctx) => {
			let username = ctx.params.username;
			let password = ctx.params.password;
			let repeatPass = ctx.params.repeatPass;
			if (!/^[a-zA-Z]{3,}$/.test(username)) {
				notify.showError('Username should be at least 3 characters long and contains only english alphabet letters!');
			} else if (!/^[a-zA-Z0-9]{6,}$/.test(password)) {
				notify.showError('Password should be at least 6 characters long and contains only digits and english alphabet letters!');
			} else if (password !== repeatPass) {
				notify.showError('Passwords must match!');
			} else {
				auth.register(username, password).then(function(userData) {
					auth.saveSession(userData);
					notify.showInfo('User registration successful.');
					ctx.redirect('#/catalog');
				}).catch(notify.handleError);
			}
		});
		this.post('#/login', (ctx) => {
			let username = ctx.params.username;
			let password = ctx.params.password;
			if (username.trim() === '' || password.trim() === '') {
				notify.showError('All field should be non-empty!');
			} else {
				auth.login(username, password).then(function(userData) {
					auth.saveSession(userData);
					notify.showInfo('Login successful.');
					ctx.redirect('#/catalog');
				}).catch(notify.handleError);
			}
		});
		this.get('#/logout', (ctx) => {
			auth.logout().then(() => {
				sessionStorage.clear();
				ctx.redirect('#/home');
			}).catch(notify.handleError);
		});
		this.get('#/catalog', (ctx) => {
			if (!auth.isAuth()) {
				ctx.redirect('#/home');
				return;
			}
			
			posts.getAllPosts().then((posts) => {
				posts.forEach((p, i) => {
					p.rank = i + 1;
					p.date = calcTime(p._kmd.ect);
					p.isAuthor = p._acl.creator === sessionStorage.getItem('id');
				});
				ctx.isAuth = auth.isAuth();
				ctx.username = sessionStorage.getItem('username');
				ctx.posts = posts;
				ctx.loadPartials({
					header: './templates/common/header.hbs',
					footer: './templates/common/footer.hbs',
					navigation: './templates/common/navigation.hbs',
					postList: './templates/posts/post-list.hbs',
					post: './templates/posts/post.hbs'
				}).then(function() {
					this.partial('./templates/posts/catalog-page.hbs');
				});
			}).catch(notify.handleError);
		});
		this.get('#/create/post', (ctx) => {
			if (!auth.isAuth()) {
				ctx.redirect('#/home');
				return;
			}
			
			ctx.isAuth = auth.isAuth();
			ctx.username = sessionStorage.getItem('username');
			ctx.loadPartials({
				header: './templates/common/header.hbs',
				footer: './templates/common/footer.hbs',
				navigation: './templates/common/navigation.hbs',
				createPostForm: './templates/forms/createPostForm.hbs'
			}).then(function() {
				this.partial('./templates/posts/create-post-page.hbs');
			});
		});
		this.post('#/create/post', (ctx) => {
			if (!auth.isAuth()) {
				ctx.redirect('#/home');
				return;
			}
			
			let author = sessionStorage.getItem('username');
			let url = ctx.params.url;
			let imageUrl = ctx.params.imageUrl;
			let title = ctx.params.title;
			let description = ctx.params.description;
			if (posts.isPostValid(title, url)) {
				posts.createPost(author, title, description, url, imageUrl).then(function() {
					notify.showInfo('Post created.');
					ctx.redirect('#/catalog');
				}).catch(notify.handleError);
			} else return;
		});
		this.get('#/edit/post/:postId', (ctx) => {
			if (!auth.isAuth()) {
				ctx.redirect('#/home');
				return;
			}
			
			let postId = ctx.params.postId;
			posts.getPostById(postId).then(function(post) {
				ctx.isAuth = auth.isAuth();
				ctx.username = sessionStorage.getItem('username');
				ctx.post = post;
				ctx.loadPartials({
					header: './templates/common/header.hbs',
					footer: './templates/common/footer.hbs',
					navigation: './templates/common/navigation.hbs'
				}).then(function() {
					this.partial('./templates/posts/edit-post-page.hbs');
				});
			}).catch(notify.handleError);
		});
		this.post('#/edit/post', (ctx) => {
			if (!auth.isAuth()) {
				ctx.redirect('#/home');
				return;
			}
			
			let postId = ctx.params.postId;
			let author = sessionStorage.getItem('username');
			let url = ctx.params.url;
			let imageUrl = ctx.params.imageUrl;
			let title = ctx.params.title;
			let description = ctx.params.description;
			if (posts.isPostValid(title, url)) {
				posts.editPost(postId, author, title, description, url, imageUrl).then(() => {
					notify.showInfo(`Post ${title} updated.`);
					ctx.redirect('#/catalog');
				}).catch(notify.handleError);
			} else return;
		});
		this.get('#/delete/post/:postId', (ctx) => {
			if (!auth.isAuth()) {
				ctx.redirect('#/home');
				return;
			}
			
			let postId = ctx.params.postId;
			posts.deletePost(postId).then(() => {
				notify.showInfo('Post deleted.');
				ctx.redirect('#/catalog');
			}).catch(notify.handleError);
		});
		this.get('#/posts', (ctx) => {
			if (!auth.isAuth()) {
				ctx.redirect('#/home');
				return;
			}
			
			posts.getMyPosts(sessionStorage.getItem('username')).then((posts) => {
				posts.forEach((p, i) => {
					p.rank = i + 1;
					p.date = calcTime(p._kmd.ect);
					p.isAuthor = p._acl.creator === sessionStorage.getItem('id');
				});
				ctx.isAuth = auth.isAuth();
				ctx.username = sessionStorage.getItem('username');
				ctx.posts = posts;
				ctx.loadPartials({
					header: './templates/common/header.hbs',
					footer: './templates/common/footer.hbs',
					navigation: './templates/common/navigation.hbs',
					postList: './templates/posts/post-list.hbs',
					post: './templates/posts/post.hbs'
				}).then(function() {
					this.partial('./templates/posts/my-post-page.hbs');
				});
			}).catch(notify.handleError);
		});
		this.get('#/details/:postId', (ctx) => {
			if (!auth.isAuth()) {
				ctx.redirect('#/home');
				return;
			}
			
			let postId = ctx.params.postId;
			let postPromise = posts.getPostById(postId);
			let allCommentsPromise = comments.getPostComments(postId);
			Promise.all([postPromise, allCommentsPromise]).then(([post, comments]) => {
				post.date = calcTime(post._kmd.ect);
				post.isAuthor = post._acl.creator === sessionStorage.getItem('id');
				comments.forEach(c => {
					c.date = calcTime(c._kmd.ect);
					c.commentAuthor = c._acl.creator === sessionStorage.getItem('id');
				});
				ctx.isAuth = auth.isAuth();
				ctx.username = sessionStorage.getItem('username');
				ctx.post = post;
				ctx.comments = comments;
				ctx.loadPartials({
					header: './templates/common/header.hbs',
					footer: './templates/common/footer.hbs',
					navigation: './templates/common/navigation.hbs',
					postDetails: './templates/details/postDetails.hbs',
					comment: './templates/details/comment.hbs'
				}).then(function() {
					this.partial('./templates/details/postDetailsPage.hbs');
				});
			}).catch(notify.showError);
		});
		this.post('#/create/comment', (ctx) => {
			if (!auth.isAuth()) {
				ctx.redirect('#/home');
				return;
			}
			
			let author = sessionStorage.getItem('username');
			let content = ctx.params.content;
			let postId = ctx.params.postId;
			if (content.trim() === '') {
				notify.showError('Cannot add empty comment.');
				return;
			}
			
			comments.createComment(postId, content, author).then(() => {
				notify.showInfo('Comment created.');
				ctx.redirect(`#/details/${postId}`);
			}).catch(notify.handleError);
		});
		this.get('#/comment/delete/:commentId/post/:postId', (ctx) => {
			if (!auth.isAuth()) {
				ctx.redirect('#/home');
				return;
			}
			
			let commentId = ctx.params.commentId;
			let postId = ctx.params.postId;
			comments.deleteComment(commentId).then(() => {
				notify.showInfo('Comment deleted.');
				ctx.redirect(`#/details/${postId}`);
			}).catch(notify.handleError);
		});
		function getWelcomePage(ctx) {
			if (!auth.isAuth()) {
				ctx.loadPartials({
					header: './templates/common/header.hbs',
					footer: './templates/common/footer.hbs',
					loginForm: './templates/forms/loginForm.hbs',
					registerForm: './templates/forms/registerForm.hbs'
				}).then(function() {
					this.partial('./templates/welcome-anonymous.hbs');
				});
			} else {
				ctx.redirect('#/catalog');
			}
		}
	});
	app.run();
	function calcTime(dateIsoFormat) {
		let diff = new Date - (new Date(dateIsoFormat));
		diff = Math.floor(diff / 60000);
		if (diff < 1) return 'less than a minute';
		if (diff < 60) return diff + ' minute' + pluralize(diff);
		diff = Math.floor(diff / 60);
		if (diff < 24) return diff + ' hour' + pluralize(diff);
		diff = Math.floor(diff / 24);
		if (diff < 30) return diff + ' day' + pluralize(diff);
		diff = Math.floor(diff / 30);
		if (diff < 12) return diff + ' month' + pluralize(diff);
		diff = Math.floor(diff / 12);
		return diff + ' year' + pluralize(diff);
		function pluralize(value) {
			if (value !== 1) return 's';
			else return '';
		}
	}
});