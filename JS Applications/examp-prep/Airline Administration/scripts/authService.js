let auth = (function() {
	function isAuth() {
		return sessionStorage.getItem('authtoken') !== null;
	}
	
	function saveSession(userData) {
		sessionStorage.setItem('authtoken', userData._kmd.authtoken);
		sessionStorage.setItem('username', userData.username);
		sessionStorage.setItem('id', userData._id);
	}
	
	function register(username, password) {
		return remote.post('user', '', 'basic', { username, password });
	}
	
	function login(username, password) {
		return remote.post('user', 'login', 'basic', { username, password });
	}
	
	function logout() {
		return remote.post('user', '_logout', 'kinvey');
	}
	
	return { isAuth, saveSession, register, login, logout };
}());