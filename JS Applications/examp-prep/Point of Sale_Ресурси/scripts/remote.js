let remote = (function () {
	const baseUrl = 'https://baas.kinvey.com/';
	const appKey = 'kid_HkGGrZ_BQ';
	const appSecrete = 'dd10d9fbc21c484dbf628db1d9f59749';
	
	function makeAuth(type) {
		if (type === 'basic') {
			return `Basic ${btoa(appKey + ':' + appSecrete)}`;
		}
		
		return `Kinvey ${sessionStorage.getItem('authtoken')}`;
	}
	
	function getRequestObject(method, module, url, auth, hasContentType) {
		let returnObject = {
			method,
			url: `${baseUrl}${module}/${appKey}/${url}`,
			headers: { 'Authorization': makeAuth(auth) }
		}
		
		if (hasContentType) {
			returnObject.headers['Content-Type'] = 'application/json';
		}
		
		return returnObject;
	}
	
	function get(module, url, auth, hasContentType) {
		return $.ajax(getRequestObject('GET', module, url, auth));
	}
	
	function post(module, url, auth, data, hasContentType) {
		let request = getRequestObject('POST', module, url, auth);
		request.data = data;
		return $.ajax(request);
	}
	
	function update(module, url, auth, data, hasContentType) {
		let request = getRequestObject('PUT', module, url, auth);
		request.data = data;
		return $.ajax(request);
	}
	
	function remove(module, url, auth, hasContentType) {
		return $.ajax(getRequestObject('DELETE', module, url, auth));
	}
	
	return { get, post, update, remove };
}());