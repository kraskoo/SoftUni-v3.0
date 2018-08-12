let service = (function() {
	function listAllCars() {
		let endPoint = 'cars?query={}&sort={"_kmd.ect": -1}';
		return remote.get('appdata', endPoint, 'kinvey');
	}
	
	function getCarById(id) {
		let endPoint = `cars/${id}`;
		return remote.get('appdata', endPoint, 'kinvey');
	}
	
	function createCarListing(title, description, imageUrl, brand, model, fuel, year, price, seller) {
		let data = { title, description, imageUrl, brand, model, fuel, year, price, seller };
		return remote.post('appdata', 'cars', 'kinvey', data);
	}
	
	function editCarListing(id, title, description, imageUrl, brand, model, fuel, year, price, seller) {
		let data = { title, description, imageUrl, brand, model, fuel, year, price, seller };
		let endPoint = `cars/${id}`;
		return remote.update('appdata', endPoint, 'kinvey', data);
	}
	
	function deleteCarListing(id) {
		let endPoint = `cars/${id}`;
		return remote.remove('appdata', endPoint, 'kinvey');
	}
	
	function userCarListings(username) {
		let endPoint = `cars?query={"seller":"${username}"}&sort={"_kmd.ect": -1}`;
		return remote.get('appdata', endPoint, 'kinvey');
	}
	
	return { listAllCars, getCarById, createCarListing, editCarListing, deleteCarListing, userCarListings };
}());