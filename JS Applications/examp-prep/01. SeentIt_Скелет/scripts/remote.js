let remote = (function () {
	const baseUrl = 'https://baas.kinvey.com/';
	const appKey = 'kid_BkwsxKBSQ';
	const appSecrete = 'ea250a9582994c6fb90499dc9c828ff9';
	
	function makeAuth(type) {
		if (type === 'basic') {
			return `Basic ${btoa(appKey + ':' + appSecrete)}`;
		}
		
		return `Kinvey ${sessionStorage.getItem('authtoken')}`;
	}
	
	function getRequestObject(method, module, url, auth) {
		return {
			method,
			url: `${baseUrl}${module}/${appKey}/${url}`,
			headers: { 'Authorization': makeAuth(auth) }
		}
	}
	
	function get(module, url, auth) {
		return $.ajax(getRequestObject('GET', module, url, auth));
	}
	
	function post(module, url, auth, data) {
		let request = getRequestObject('POST', module, url, auth);
		request.data = data;
		return $.ajax(request);
	}
	
	function update(module, url, auth, data) {
		let request = getRequestObject('PUT', module, url, auth);
		request.data = data;
		return $.ajax(request);
	}
	
	function remove(module, url, auth) {
		return $.ajax(getRequestObject('DELETE', module, url, auth));
	}
	
	return { get, post, update, remove };
}());