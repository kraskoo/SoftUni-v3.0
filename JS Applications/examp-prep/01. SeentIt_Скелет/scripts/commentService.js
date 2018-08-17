let comments = (function() {
	function getPostComments(postId) {
		let endPoint = `comments?query={"postId":"${postId}"}&sort={"_kmd.ect": -1}`;
		return remote.get('appdata', endPoint, 'kinvey');
	}
	
	function createComment(postId, content, author) {
		let endPoint = 'comments';
		let data = { postId, content, author };
		return remote.post('appdata', endPoint, 'kinvey', data);
	}
	
	function deleteComment(commentId) {
		let endPoint = `comments/${commentId}`;
		return remote.remove('appdata', endPoint, 'kinvey');
	}
	
	return { getPostComments, createComment, deleteComment };
}());