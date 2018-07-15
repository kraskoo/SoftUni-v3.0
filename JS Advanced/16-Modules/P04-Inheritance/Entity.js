class Entity {
	constructor(name) {
		if (new.target === Entity) {
			throw new Error('An abstract class of Entity cannot instantiate directly!');
		}

		this.name = name;
	}
}

module.exports = Entity;