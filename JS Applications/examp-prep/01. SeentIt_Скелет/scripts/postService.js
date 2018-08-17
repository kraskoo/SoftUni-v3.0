let posts = (function() {
	function getAllPosts() {
		let endPoint = 'posts?query={}&sort={"_kmd.ect": -1}';
		return remote.get('appdata', endPoint, 'kinvey');
	}
	
	function createPost(author, title, description, url, imageUrl) {
		let data = { author, title, description, url, imageUrl };
		return remote.post('appdata', 'posts', 'kinvey', data);
	}
	
	function editPost(postId, author, title, description, url, imageUrl) {
		let endPoint = `posts/${postId}`;
		let data = { author, title, description, url, imageUrl };
		return remote.update('appdata', endPoint, 'kinvey', data);
	}
	
	function deletePost(postId) {
		let endPoint = `posts/${postId}`;
		return remote.remove('appdata', endPoint, 'kinvey');
	}
	
	function getMyPosts(username) {
		let endPoint = `posts?query={"author":"${username}"}&sort={"_kmd.ect": -1}`;
		return remote.get('appdata', endPoint, 'kinvey');
	}
	
	function getPostById(postId) {
		let endPoint = `posts/${postId}`;
		return remote.get('appdata', endPoint, 'kinvey');
	}
	
	function isPostValid(title, url) {
		if (title.trim() === '') {
			notify.showError('Title is required!');
		} else if (url.trim() === '') {
			notify.showError('Url is required!');
		} else if (!url.startsWith('http')) {
			notify.showError('Url should be valid link!');
		} else {
			return true;
		}

		return false;
	}
	
	return { getAllPosts, createPost, editPost, deletePost, getMyPosts, getPostById, isPostValid };
}());