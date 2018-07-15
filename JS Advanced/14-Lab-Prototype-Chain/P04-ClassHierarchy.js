function solve() {
	class Figure {
		constructor() {
			if (new.target === Figure) {
				throw new Error('Cannot instantiate an abstract class');
			}
		}
		
		get area() {
			return undefined;
		}
		
		toString() {
			return this.constructor.name;
		}
	}
	
	class Circle extends Figure {
		constructor(r) {
			super();
			this.radius = r;
		}
		
		get area() {
			return Math.PI * this.radius * this.radius;
		}
		
		toString() {
			return `${super.toString()} - radius: ${this.radius}`;
		}
	}
	
	class Rectangle extends Figure {
		constructor(w, h) {
			super();
			this.width = w;
			this.height = h;
		}
		
		get area() {
			return this.width * this.height;
		}
		
		toString() {
			return `${super.toString()} - width: ${this.width}, height: ${this.height}`;
		}
	}
	
	return { Figure, Circle, Rectangle };
}