class Rat {
	constructor(name) {
		this.name = name;
		this.unitedRats = [];
	}
	
	unite(otherRat) {
		if (otherRat instanceof Rat) {
			this.unitedRats.push(otherRat);
		}
	}
	
	getRats() {
		return this.unitedRats;
	}
	
	toString() {
		let output = this.name;
		for (let rat of this.unitedRats) {
			output += `\n##${rat.name}`;
		}
		
		return output;
	}
}