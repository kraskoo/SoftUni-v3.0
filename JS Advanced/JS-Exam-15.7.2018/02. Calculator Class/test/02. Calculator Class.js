const expect = require('chai').expect;
const Calculator = require('../02. Calculator Class');

describe('Calculator', function() {
	let calc;
	beforeEach(() => {
		calc = new Calculator();
	});
	describe('constructor', function() {
		it('initial class should have an empty array', function() {
			expect(JSON.stringify(calc.expenses)).to.be.equal(JSON.stringify([]));
			expect(calc.expenses.length).to.be.equal(0);
		});
		it('initial class should have all functions and properties as well', function() {
			expect(calc.hasOwnProperty('expenses')).to.be.equal(true);
			expect(Object.getPrototypeOf(calc).hasOwnProperty('add')).to.be.equal(true);
			expect(Object.getPrototypeOf(calc).hasOwnProperty('divideNums')).to.be.equal(true);
			expect(Object.getPrototypeOf(calc).hasOwnProperty('toString')).to.be.equal(true);
			expect(Object.getPrototypeOf(calc).hasOwnProperty('orderBy')).to.be.equal(true);
		});
	});
	describe('add', function() {
		it('add should push correctly', function() {
			calc.add(1);
			calc.add(2);
			calc.add(3);
			let result = [ 1, 2, 3 ];
			expect(calc.expenses.length).to.be.equal(3);
			expect(JSON.stringify(calc.expenses)).to.be.equal(JSON.stringify(result));
			for (let i = 0; i < result.length; i++) {
				expect(calc.expenses[i]).to.be.equal(result[i]);
			}
		});
	});
	describe('divideNums', function() {
		it('should return correct result, if there is only numbers in the array', function() {
			calc.add(6);
			calc.add(3);
			calc.add(2);
			expect(calc.divideNums()).to.be.equal(1);
		});
		it('should return correct result, if has numbers and strings in the array', function() {			
			calc.add(6);
			calc.add('Pesho');
			calc.add(3);
			calc.add('Gosho');
			calc.add(2);
			calc.add('Asen');
			expect(calc.divideNums()).to.be.equal(1);
		});
		it('should return "Cannot divide by zero", if any number is zero', function() {
			calc.add(6);
			calc.add(3);
			calc.add(0);
			expect(calc.divideNums()).to.be.equal('Cannot divide by zero');
		});
		it('should throw, if the array is empty', function() {
			expect(() => calc.divideNums()).to.throw(Error);
		});
		it('should replace all values with divided result', function() {
			calc.add(6);
			calc.add(3);
			calc.add(2);
			calc.divideNums();
			let result = [ 1 ];
			for (let i = 0; i < result.length; i++) {
				expect(calc.expenses[i]).to.be.equal(result[i]);
			}
		});
	});
	describe('orderBy', function() {
		it('should return "empty" for an empty array', function() {
			expect(calc.orderBy()).to.be.equal('empty');
		});
		it('should return correct result, if the values are only numbers', function() {
			calc.add(6);
			calc.add(3);
			calc.add(2);
			expect(calc.orderBy()).to.be.equal('2, 3, 6');
		});
		it('should return correct result, if the values are from different types', function() {
			calc.add(6);
			calc.add('Pesho');
			calc.add(3);
			calc.add('Gosho');
			calc.add(2);
			calc.add('Asen');
			expect(calc.orderBy()).to.be.equal('2, 3, 6, Asen, Gosho, Pesho');
		});
	});
	describe('toString', function() {
		it('if the array has values should return result joined by " -> "', function() {
			calc.add(1);
			calc.add(2);
			calc.add(3);
			let result = [ 1, 2, 3 ];
			expect(calc.toString()).to.be.equal(result.join(' -> '));
		});
		it('should return "empty array", if array is empty', function() {
			expect(calc.toString()).to.be.equal('empty array');
		});
	});
});