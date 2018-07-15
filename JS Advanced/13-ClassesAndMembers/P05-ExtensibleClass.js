(function () {
	let currentId = 0;
	return class Extensible {
		constructor() {
			this.id = currentId;
			currentId++;
		}
		
		extend(template) {
			for (let prop of Object.keys(template)) {
				if (typeof(template[prop]) === 'function') {
					Object.getPrototypeOf(this)[prop] = template[prop];
				} else {
					this[prop] = template[prop];
				}
			}
		}
	}
}());