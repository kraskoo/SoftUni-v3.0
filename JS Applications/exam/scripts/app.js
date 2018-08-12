$(() => {
	const app = Sammy('#container', function() {
		this.use('Handlebars', 'hbs');
		this.get('index.html', getHome);
		this.get('/', getHome);
		this.get('#/home', getHome);
		this.get('#/login', (ctx) => {
			ctx.isAuth = auth.isAuth();
			if (ctx.isAuth) {
				ctx.redirect('#/catalog');
				return;
			}
			
			ctx.loadPartials({
				header: './templates/common/header.hbs',
				footer: './templates/common/footer.hbs'
			}).then(function() {
				this.partial('./templates/login.hbs');
			});
		});
		this.post('#/login', (ctx) => {
			ctx.isAuth = auth.isAuth();
			if (ctx.isAuth) {
				ctx.redirect('#/catalog');
				return;
			}
			
			let username = ctx.params.username;
			let password = ctx.params.password;
			if (!/^[a-zA-Z]{3,}$/.test(username)) {
				notify.showError('Username should be at least 3 characters long and contains only english alpabets.');
			} else if (!/^[a-zA-Z0-9]{6,}$/.test(password)) {
				notify.showError('Password should be at least 6 characters long and contains only english alpabets and digits.');
			} else {
				auth.login(username, password).then((userData) => {
					auth.saveSession(userData);
					notify.showInfo('Login successful.');
					ctx.redirect('#/catalog');
				}).catch(notify.handleError);
			}
		});
		this.get('#/register', (ctx) => {
			ctx.isAuth = auth.isAuth();
			if (ctx.isAuth) {
				ctx.redirect('#/catalog');
				return;
			}
			
			ctx.loadPartials({
				header: './templates/common/header.hbs',
				footer: './templates/common/footer.hbs'
			}).then(function() {
				this.partial('./templates/register.hbs');
			});
		});
		this.post('#/register', (ctx) => {
			ctx.isAuth = auth.isAuth();
			if (ctx.isAuth) {
				ctx.redirect('#/catalog');
				return;
			}
			
			let username = ctx.params.username;
			let password = ctx.params.password;
			let repeatPass = ctx.params.repeatPass;
			if (!/^[a-zA-Z]{3,}$/.test(username)) {
				notify.showError('Username should be at least 3 characters long and contains only english alpabets.');
			} else if (!/^[a-zA-Z0-9]{6,}$/.test(password)) {
				notify.showError('Password should be at least 6 characters long and contains only english alpabets and digits.');
			} else if (password !== repeatPass) {
				notify.showError('Passwords should match.');
			} else {
				auth.register(username, password).then((userData) => {
					auth.saveSession(userData);
					notify.showInfo('User registration successful.');
					ctx.redirect('#/catalog');
				}).catch(notify.handleError);
			}
		});
		this.get('#/logout', (ctx) => {
			ctx.isAuth = auth.isAuth();
			if (ctx.isAuth) {
				ctx.redirect('#/catalog');
				return;
			}
			
			auth.logout().then(() => {
				sessionStorage.clear();
				notify.showInfo('Logout successful.');
				ctx.redirect('#/login');
			}).catch(notify.handleError);
		});
		this.get('#/catalog', (ctx) => {
			ctx.isAuth = auth.isAuth();
			if (!ctx.isAuth) {
				ctx.redirect('#/home');
				return;
			}
			
			ctx.username = sessionStorage.getItem('username');
			service.listAllCars().then((cars) => {
				cars.forEach(c => {
					c.isAuthor = c._acl.creator === sessionStorage.getItem('id');
				});
				ctx.cars = cars;
				ctx.loadPartials({
					header: './templates/common/header.hbs',
					footer: './templates/common/footer.hbs',
					car: './templates/car.hbs'
				}).then(function() {
					this.partial('./templates/catalog.hbs');
				});
			}).catch(notify.handleError);
		});
		this.get('#/create/car', (ctx) => {
			ctx.isAuth = auth.isAuth();
			if (!ctx.isAuth) {
				ctx.redirect('#/home');
				return;
			}
			
			ctx.username = sessionStorage.getItem('username');
			ctx.loadPartials({
					header: './templates/common/header.hbs',
					footer: './templates/common/footer.hbs'
			}).then(function() {
				this.partial('./templates/create-car.hbs');
			});
		});
		this.post('#/create/car', (ctx) => {
			ctx.isAuth = auth.isAuth();
			if (!ctx.isAuth) {
				ctx.redirect('#/home');
				return;
			}
			
			let brand = ctx.params.brand;
			let description = ctx.params.description;
			let fuelType = ctx.params.fuelType;
			let imageUrl = ctx.params.imageUrl;
			let model = ctx.params.model;
			let price = +ctx.params.price;
			let title = ctx.params.title;
			let year = ctx.params.year;
			let seller = sessionStorage.getItem('username');
			if (isValidCar(title, description, brand, fuelType, model, year, price, imageUrl)) {
				year = +year;
				service.createCarListing(title, description, imageUrl, brand, model, fuelType, year, price, seller).then(() => {
					notify.showInfo('listing created.');
					ctx.redirect('#/catalog');
				}).catch(notify.handleError);
			}
		});
		this.get('#/user/cars', (ctx) => {
			ctx.isAuth = auth.isAuth();
			if (!ctx.isAuth) {
				ctx.redirect('#/home');
				return;
			}
			
			ctx.username = sessionStorage.getItem('username');
			service.userCarListings(ctx.username).then((cars) => {
				cars.forEach((c) => {
					c.fuelType = c.fuel;
				});
				ctx.cars = cars;
				ctx.loadPartials({
					header: './templates/common/header.hbs',
					footer: './templates/common/footer.hbs',
					car: './templates/car-user.hbs'
				}).then(function() {
					this.partial('./templates/user-catalog.hbs');
				});
			}).catch(notify.handleError);
		});
		this.get('#/car/details/:carId', (ctx) => {
			ctx.isAuth = auth.isAuth();
			if (!ctx.isAuth) {
				ctx.redirect('#/home');
				return;
			}
			
			ctx.username = sessionStorage.getItem('username');
			let id = ctx.params.carId;
			service.getCarById(id).then((car) => {
				car.isAuthor = car._acl.creator === sessionStorage.getItem('id');
				car.fuelType = car.fuel;
				ctx.car = car;
				ctx.loadPartials({
					header: './templates/common/header.hbs',
					footer: './templates/common/footer.hbs'
				}).then(function() {
					this.partial('./templates/car-details.hbs');
				});
			}).catch(notify.handleError);
		});
		this.get('#/car/edit/:carId', (ctx) => {
			ctx.isAuth = auth.isAuth();
			if (!ctx.isAuth) {
				ctx.redirect('#/home');
				return;
			}
			
			ctx.username = sessionStorage.getItem('username');
			let id = ctx.params.carId;
			service.getCarById(id).then((car) => {
				car.fuelType = car.fuel;
				ctx.car = car;
				ctx.loadPartials({
					header: './templates/common/header.hbs',
					footer: './templates/common/footer.hbs'
				}).then(function() {
					this.partial('./templates/edit-car.hbs');
				});
			}).catch(notify.handleError);
		});
		this.post('#/car/edit', (ctx) => {
			ctx.isAuth = auth.isAuth();
			if (!ctx.isAuth) {
				ctx.redirect('#/home');
				return;
			}
			
			ctx.username = sessionStorage.getItem('username');
			let id = ctx.params.carId;
			let brand = ctx.params.brand;
			let description = ctx.params.description;
			let fuelType = ctx.params.fuelType;
			let imageUrl = ctx.params.imageUrl;
			let model = ctx.params.model;
			let price = ctx.params.price;
			let title = ctx.params.title;
			let year = ctx.params.year;
			let seller = sessionStorage.getItem('username');
			if (isValidCar(title, description, brand, fuelType, model, year, price, imageUrl)) {
				year = +year;
				service.editCarListing(id, title, description, imageUrl, brand, model, fuelType, year, price, seller).then(() => {
					notify.showInfo(`Listing ${title} updated.`);
					ctx.redirect('#/catalog');
				}).catch(notify.handleError);
			}
		});
		this.get('#/car/delete/:carId', (ctx) => {
			ctx.isAuth = auth.isAuth();
			if (!ctx.isAuth) {
				ctx.redirect('#/home');
				return;
			}
			
			ctx.username = sessionStorage.getItem('username');
			service.deleteCarListing(ctx.params.carId).then(() => {
				notify.showInfo('Listing deleted.');
				ctx.redirect('#/catalog');
			}).catch(notify.handleError);
		});
		function getHome(ctx) {
			ctx.isAuth = auth.isAuth();
			if (ctx.isAuth) {
				ctx.username = sessionStorage.getItem('username');
				ctx.redirect('#/catalog');
				return;
			}
			
			ctx.loadPartials({
				header: './templates/common/header.hbs',
				footer: './templates/common/footer.hbs'
			}).then(function() {
				this.partial('./templates/home.hbs');
			});
		}
	});
	app.run();
	function isValidCar(title, description, brand, fuelType, model, year, price, imageUrl) {
		if (title.length > 33) {
			notify.showError('Title should not exceed 33 characters long.');
		} else if (description.length < 30 || description.length > 450) {
			notify.showError('Description should be in [30.. 450] range.');
		} else if (brand.length > 11) {
			notify.showError('Brand should not exceed 11 characters long.');
		} else if (fuelType.length > 11) {
			notify.showError('Fuel type should not exceed 11 characters long.');
		} else if (model.length < 4 || model.length > 11) {
			notify.showError('Model should be in [4.. 11] range.');
		} else if (year.length < 4) {
			notify.showError('Year should at least 4 characters long.');
		} else if (price > 1000000) {
			notify.showError('Price should not exceed 1000000.');
		} else if (!imageUrl.startsWith('http')) {
			notify.showError('Image url should start with "http"');
		} else {
			return true;
		}
		
		return false;
	}
});