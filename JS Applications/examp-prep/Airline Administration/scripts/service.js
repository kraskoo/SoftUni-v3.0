let service = (function() {
	const monthNames = [ "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" ];
	function getPublishedFlights() {
		let endPoint = 'flights?query={"isPublished":"true"}';
		return remote.get('appdata', endPoint, 'kinvey');
	}
	
	function createFlight(destionantion, origin, departureDate, departureTime, seats, cost, image, isPublished) {
		let data = { destionantion, origin, departureDate, departureTime, seats, cost, image, isPublished };
		return remote.post('appdata', 'flights', 'kinvey', data);
	}
	
	function editFlight(flightId, destionantion, origin, departureDate, departureTime, seats, cost, image, isPublished) {
		let data = { destionantion, origin, departureDate, departureTime, seats, cost, image, isPublished };
		let endPoint = `flights/${flightId}`;
		return remote.update('appdata', endPoint, 'kinvey', data);
	}
	
	function deleteFlight(flightId) {
		let endPoint = `flights/${flightId}`;
		return remote.remove('appdata', endPoint, 'kinvey');
	}
	
	function flightDetails(flightId) {
		let endPoint = `flights/${flightId}`;
		return remote.get('appdata', endPoint, 'kinvey');
	}
	
	function userFlights(userId) {
		let endPoint = `flights?query={"_acl.creator":"${userId}"}`;
		return remote.get('appdata', endPoint, 'kinvey');
	}
	
	return { getPublishedFlights, createFlight, editFlight, deleteFlight, flightDetails, userFlights, monthNames };
}());