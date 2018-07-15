(function () {
	let id, e, f, l;
	return class CheckingAccount {
		constructor(clientId, email, firstName, lastName) {
			id = e = f = l = undefined;
			this.clientId = clientId;
			this.email = email;
			this.firstName = firstName;
			this.lastName = lastName;
			this.products = [];
		}
		
		set clientId(value) {
			if (!/^\d{6}$/.test(value)) {
				throw new TypeError('Client ID must be a 6-digit number');
			}
		
			id = value;
		}
		
		get clientId() {
			return id;
		}
		
		set email(value) {
			if (!/^[a-zA-Z0-9]+?@[a-zA-Z.]+$/.test(value)) {
				throw new TypeError('Invalid e-mail');
			}
		
			e = value;
		}
		
		get email() {
			return e;
		}
		
		set firstName(value) {
			if (value.length < 3 || value.length > 20) {
				throw new TypeError('First name must be between 3 and 20 characters long');
			}
			
			if (!/^[a-zA-Z]{3,20}$/.test(value)) {
				throw new TypeError('First name must contain only Latin characters');
			}
		
			f = value;
		}
		
		get firstName() {
			return f;
		}
		
		set lastName(value) {
			if (value.length < 3 || value.length > 20) {
				throw new TypeError('Last name must be between 3 and 20 characters long');
			}
		
			if (!/^[a-zA-Z]{3,20}$/.test(value)) {
				throw new TypeError('Last name must contain only Latin characters');
			}
		
			l = value;
		}
		
		get lastName() {
			return l;
		}
	}
}());