$(() => {
	const app = Sammy('#container', function() {
		this.use('Handlebars', 'hbs');
		this.get('index.html', getHome);
		this.get('/', getHome);
		this.get('#/home', getHome);
		this.get('#/register', (ctx) => {
			ctx.isAuth = auth.isAuth();
			ctx.loadPartials({
				header: './templates/common/header.hbs',
				footer: './templates/common/footer.hbs'
			}).then(function() {
				this.partial('./templates/register.hbs');
			});
		});
		this.post('#/register', (ctx) => {
			let username = ctx.params.username;
			let password = ctx.params.pass;
			let checkPass = ctx.params.checkPass;
			if (!/^[a-zA-Z]{5,}$/.test(username)) {
				notify.showError('Username should be at least 5 characters long and contains only english alphabets!');
			} else if (password.trim() === '') {
				notify.showError('Password should be non-empty string!');
			} else if (password !== checkPass) {
				notify.showError('Passwords must match!');
			} else {
				auth.register(username, password).then((userData) => {
					auth.saveSession(userData);
					notify.showInfo('User registration successful.');
					ctx.redirect('#/catalog');
				}).catch(notify.handleError);
			}
		});
		this.get('#/login', (ctx) => {
			ctx.isAuth = auth.isAuth();
			ctx.loadPartials({
				header: './templates/common/header.hbs',
				footer: './templates/common/footer.hbs'
			}).then(function() {
				this.partial('./templates/login.hbs');
			});
		});
		this.post('#/login', (ctx) => {
			let username = ctx.params.username;
			let password = ctx.params.pass;
			if (!/^[a-zA-Z]{5,}$/.test(username)) {
				notify.showError('Username should be at least 5 characters long and contains only english alphabets!');
			} else if (password.trim() === '') {
				notify.showError('Password should be non-empty string!');
			} else {
				auth.login(username, password).then((userData) => {
					auth.saveSession(userData);
					notify.showInfo('Login successful.');
					ctx.redirect('#/catalog');
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
		this.get('#/catalog', (ctx) => {
			let isAuth = auth.isAuth();
			if (!isAuth) {
				ctx.redirect('#/login');
				return;
			}
			
			service.getPublishedFlights().then((flights) => {
				flights.forEach((f) => {
					f.date = getFormmatedDate(f.departureDate);
				});
				ctx.flights = flights;
				ctx.isAuth = isAuth;
				ctx.username = sessionStorage.getItem('username');
				ctx.loadPartials({
					header: './templates/common/header.hbs',
					footer: './templates/common/footer.hbs',
					flight: './templates/flight.hbs'
				}).then(function() {
					this.partial('./templates/catalog.hbs');
				});
			}).catch(notify.handleError);
		});
		this.get('#/add/flight', (ctx) => {
			let isAuth = auth.isAuth();
			if (!isAuth) {
				ctx.redirect('#/login');
				return;
			}
			
			ctx.isAuth = isAuth;
			ctx.username = sessionStorage.getItem('username');
			ctx.loadPartials({
				header: './templates/common/header.hbs',
				footer: './templates/common/footer.hbs'
			}).then(function() {
				this.partial('./templates/add-flight.hbs');
			});
		});
		this.post('#/add/flight', (ctx) => {
			if (!auth.isAuth()) {
				ctx.redirect('#/login');
				return;
			}
			
			let destination = ctx.params.destination;
			let origin = ctx.params.origin;
			let departureDate = ctx.params.departureDate;
			let departureTime = ctx.params.departureTime;
			let seats = +ctx.params.seats;
			let cost = +ctx.params.cost;
			let img = ctx.params.img;
			let isPublic = ctx.params.public && ctx.params.public === 'on' ? true : false;
			if (destination.trim() === '') {
				notify.showError('Destination should be non-empty string.');
			} else if (origin.trim() === '') {
				notify.showError('Origin should be non-empty string.');
			} else if (seats <= 0) {
				notify.showError('Seats should be positive number.');
			} else if (cost <= 0) {
				notify.showError('Cost should be positive number.');
			} else {
				service.createFlight(destination, origin, departureDate, departureTime, seats, cost, img, isPublic).then(() => {
					notify.showInfo('Created flight.');
					ctx.redirect('#/catalog');
				}).catch(notify.handleError);
			}
		});
		this.get('#/flight/details/:flightId', (ctx) => {
			if (!auth.isAuth()) {
				ctx.redirect('#/login');
				return;
			}
			
			service.flightDetails(ctx.params.flightId).then((flight) => {
				ctx.isAuth = auth.isAuth();
				ctx.username = sessionStorage.getItem('username');
				flight.date = getFormmatedDate(flight.departureDate);
				flight.isCreator = flight._acl.creator === sessionStorage.getItem('id');
				ctx.flight = flight;
				ctx.loadPartials({
					header: './templates/common/header.hbs',
					footer: './templates/common/footer.hbs'
				}).then(function() {
					this.partial('./templates/flight-details.hbs');
				});
			}).catch(notify.handleError);
		});
		this.get('#/edit/flight/:flightId', (ctx) => {
			if (!auth.isAuth()) {
				ctx.redirect('#/login');
				return;
			}
			
			service.flightDetails(ctx.params.flightId).then((flight) => {
				ctx.isAuth = auth.isAuth();
				ctx.username = sessionStorage.getItem('username');
				ctx.flight = flight;
				ctx.loadPartials({
					header: './templates/common/header.hbs',
					footer: './templates/common/footer.hbs'
				}).then(function() {
					this.partial('./templates/edit-flight.hbs');
				});
			}).catch(notify.handleError);
		});
		this.post('#/edit/flight/:flightId', (ctx) => {
			if (!auth.isAuth()) {
				ctx.redirect('#/login');
				return;
			}
			
			let id = ctx.params.flightId;
			let destination = ctx.params.destination;
			let origin = ctx.params.origin;
			let departureDate = ctx.params.departureDate;
			let departureTime = ctx.params.departureTime;
			let seats = +ctx.params.seats;
			let cost = +ctx.params.cost;
			let img = ctx.params.img;
			let isPublic = ctx.params.public && ctx.params.public === 'on' ? true : false;
			if (destination.trim() === '') {
				notify.showError('Destination should be non-empty string.');
			} else if (origin.trim() === '') {
				notify.showError('Origin should be non-empty string.');
			} else if (seats <= 0) {
				notify.showError('Seats should be positive number.');
			} else if (cost <= 0) {
				notify.showError('Cost should be positive number.');
			} else {
				service.editFlight(id, destination, origin, departureDate, departureTime, seats, cost, img, isPublic).then(() => {
					notify.showInfo('Successfully edited flight.');
					ctx.redirect(`#/flight/details/${id}`);
				}).catch(notify.handleError);
			}
		});
		this.get('#/user/flights', (ctx) => {
			if (!auth.isAuth()) {
				ctx.redirect('#/login');
				return;
			}
			
			service.userFlights(sessionStorage.getItem('id')).then((flights) => {
				flights.forEach((f) => {
					f.date = getFormmatedDate(f.departureDate);
				});
				ctx.isAuth = auth.isAuth();
				ctx.username = sessionStorage.getItem('username');
				ctx.flights = flights;
				ctx.loadPartials({
					header: './templates/common/header.hbs',
					footer: './templates/common/footer.hbs',
					userFlightDetails: './templates/user-flight-details.hbs'
				}).then(function() {
					this.partial('./templates/user-flights.hbs');
				});
			}).catch(notify.handleError);
		});
		this.get('#/flight/delete/:flightId', (ctx) => {
			if (!auth.isAuth()) {
				ctx.redirect('#/login');
				return;
			}
			
			service.deleteFlight(ctx.params.flightId).then(() => {
				notify.showInfo('Flight deleted.');
				ctx.redirect('#/user/flights');
			}).catch(notify.handleError);
		});
		function getHome(ctx) {
			if (!auth.isAuth()) {
				ctx.redirect('#/login');
			} else {
				ctx.redirect('#/catalog');
			}
		}
	});
	app.run();
	function getFormmatedDate(dateString) {
		let currentDate = new Date(dateString);
		let day = currentDate.getDate();
		let month = service.monthNames[currentDate.getMonth()];
		return `${day} ${month}`;
	}
});