$(() => {
	const app = Sammy('#container', function() {
		this.use('Handlebars', 'hbs');
		this.get('index.html', getWelcome);
		this.get('/', getWelcome);
		this.get('#/home', getWelcome);
		this.post('#/login', (ctx) => {
			let username = ctx.params['username-login'];
			let password = ctx.params['password-login'];
			if (!/[a-zA-Z]{5,}/.test(username)) {
				notify.showError('Username should at least 5 characters long and contains only english alphabets.');
			} else if (password.trim() === '') {
				notify.showError('Password should be non-empty field.');
			} else {
				auth.login(username, password).then(function(userData) {
					auth.saveSession(userData);
					notify.showInfo('Login successful.');
					ctx.redirect('#/create/receipt');
				}).catch(notify.handleError);
			}
		});
		this.post('#/register', (ctx) => {
			let username = ctx.params['username-register'];
			let password = ctx.params['password-register'];
			let checkPassword = ctx.params['password-register-check'];
			if (!/[a-zA-Z]{5,}/.test(username)) {
				notify.showError('Username should at least 5 characters long and contains only english alphabets.');
			} else if (password.trim() === '') {
				notify.showError('Password should be non-empty field.');
			} else if (password !== checkPassword) {
				notify.showError('Passwords should match.');
			} else {
				auth.register(username, password).then(function(userData) {
					auth.saveSession(userData);
					notify.showInfo('User registration successful.');
					ctx.redirect('#/create/receipt');
				}).catch(notify.handleError);
			}
		});
		this.get('#/logout', (ctx) => {
			auth.logout().then(() => {
				sessionStorage.clear();
				notify.showInfo('Logout successful.');
				ctx.redirect('#/home');
			}).catch(notify.handleError);
		});
		this.get('#/create/receipt', (ctx) => {
			if (!auth.isAuth()) {
				ctx.redirect('#/home');
				return;
			}
			
			service.getActiveReceipt(sessionStorage.getItem('id')).then((data) => {
				if (data.length === 0) {
					service.createReceipt(true, 0, 0).then((data) => {
						console.log(data);
						ctx.username = sessionStorage.getItem('username');
						ctx.receiptId = data._id;
						ctx.productCount = data.productCount;
						ctx.total = data.total;
						ctx.loadPartials({
							header: './templates/common/header.hbs',
							footer: './templates/common/footer.hbs'
						}).then(function() {
							this.partial('./templates/create-receipt.hbs');
						})
					}).catch(notify.handleError);
				} else {
					data = data[0];
					let total = 0;
					let productCount = 0;
					service.getEntriesByReceiptId(data._id).then((entries) => {
						entries.forEach((e) => {
							let qty = +e.qty;
							let price = +e.price;
							e.totalPrice = +((qty * price).toFixed(2));
							productCount += qty;
							total += e.totalPrice;
							total = +(total.toFixed(2));
						});
						ctx.entries = entries;
						ctx.username = sessionStorage.getItem('username');
						ctx.receiptId = data._id;
						ctx.productCount = productCount;
						ctx.total = total;
						ctx.loadPartials({
							header: './templates/common/header.hbs',
							footer: './templates/common/footer.hbs',
							entryRow: './templates/entries/entry-row.hbs'
						}).then(function() {
							this.partial('./templates/create-receipt.hbs');
						});
					}).catch(notify.handleError);
				}
			}).catch(notify.handleError);
		});
		this.post('#/create/entry', (ctx) => {
			let receiptId = ctx.params['create-entity-receiptId'];
			let type = ctx.params.type;
			let qty = +ctx.params.qty;
			let price = +ctx.params.price;
			if (type.trim() === '') {
				notify.showError('Type should be non-empty string.');
			} else if (Number.isNaN(qty)) {
				notify.showError('Quantity should be number type.');
			} else if (Number.isNaN(price)) {
				notify.showError('Price should be number type.');
			} else {
				service.addEntry(type, qty, price, receiptId).then(() => {
					notify.showInfo('Entry added');
					ctx.redirect('#/create/receipt');
				}).catch(notify.handleError);
			}
		});
		this.get('#/delete/entry/:id', (ctx) => {
			let id = ctx.params.id;
			service.deleteEntry(id).then(() => {
				notify.showInfo('Entry removed');
				ctx.redirect('#/create/receipt');
			}).catch(notify.handleError);
		});
		this.post('#/checkout', (ctx) => {
			let receiptId = ctx.params.receiptId;
			let productCount = +ctx.params.productCount;
			let total = +ctx.params.total;
			service.updateReceipt(receiptId, false, productCount, total).then(() => {
				notify.showInfo('Receipt checked out');
				ctx.redirect('#/create/receipt');
			}).catch(notify.handleError);
		});
		this.get('#/receipts', (ctx) => {
			if (!auth.isAuth()) {
				ctx.redirect('#/home');
				return;
			}
			
			service.getMyReceipts(sessionStorage.getItem('id')).then((receiptData) => {
				let total = 0;
				receiptData.forEach((r) => {
					let currentDate = new Date(r._kmd.ect);
					let year = currentDate.getFullYear();
					let month = currentDate.getMonth() < 10 ? '0' + currentDate.getMonth() : currentDate.getMonth();
					let day = currentDate.getDate() < 10 ? '0' + currentDate.getDate() : currentDate.getDate();
					let hours = currentDate.getHours() < 10 ? '0' + currentDate.getHours() : currentDate.getHours();
					let minutes = currentDate.getMinutes() < 10 ? '0' + currentDate.getMinutes() : currentDate.getMinutes();
					r.date = `${year}-${month}-${day} ${hours}:${minutes}`;
					total += +r.total;
					total = +(total.toFixed(2));
				});
				ctx.username = sessionStorage.getItem('username');
				ctx.receipts = receiptData;
				ctx.total = total;
				ctx.loadPartials({
					header: './templates/common/header.hbs',
					footer: './templates/common/footer.hbs',
					receiptRow: './templates/receipt-row.hbs'
				}).then(function() {
					this.partial('./templates/all-receipt.hbs');
				});
			}).catch(notify.handleError);
		});
		this.get('receipt/details/:receiptId', (ctx) => {
			service.getEntriesByReceiptId(ctx.params.receiptId).then((entries) => {
				entries.forEach((e) => {
					e.total = +((+e.price) * (+e.qty)).toFixed(2);
				});
				ctx.username = sessionStorage.getItem('username');
				ctx.entries = entries;
				ctx.loadPartials({
					header: './templates/common/header.hbs',
					footer: './templates/common/footer.hbs',
					entryDetail: './templates/entries/entry-row-detail.hbs'
				}).then(function() {
					this.partial('./templates/entries/entries.hbs');
				});
			}).catch(notify.handleError);
		});
		function getWelcome(ctx) {
			if (auth.isAuth()) {
				ctx.redirect('#/create/receipt');
				return;
			}
			
			ctx.loadPartials({
				footer: './templates/common/footer.hbs'
			}).then(function() {
				this.partial('./templates/welcome-anonymous.hbs');
			});
		}
	});
	app.run();
});