describe('mathEnforcer', function() {
	describe('addFive', function() {
		it('addFive with a non-number value, should return undefined', function() {
			expect(mathEnforcer.addFive('o')).to.be.equal(undefined, 'Function returns incorrect value!');
		});
		it('addFive with a positive number value, should return correct value', function() {
			expect(mathEnforcer.addFive(6)).to.be.equal(11, 'Function returns correct value!');
		});
		it('addFive with a negative number value, should return correct value', function() {
			expect(mathEnforcer.addFive(-6)).to.be.equal(-1, 'Function returns correct value!');
		});
		it('addFive with a floating-point number, should return correct value', function() {
			expect(mathEnforcer.addFive(9.21)).to.be.equal(14.21, 'Function returns correct value!');
		});
	});
	describe('subtractTen', function() {
		it('subtractTen with a non-number value, should return undefined', function() {
			expect(mathEnforcer.subtractTen('o')).to.be.equal(undefined, 'Function returns incorrect value!');
		});
		it('subtractTen with a postive number value, should return correct value', function() {
			expect(mathEnforcer.subtractTen(18)).to.be.equal(8, 'Function returns correct value!');
		});
		it('subtractTen with a negative number value, should return correct value', function() {
			expect(mathEnforcer.subtractTen(-18)).to.be.equal(-28, 'Function returns correct value!');
		});
		it('subtractTen with a floating-point number, should return correct value', function() {
			expect(mathEnforcer.subtractTen(13.4)).to.be.closeTo(3.4, 0.004, 'Function returns correct value!');
		});
	});
	describe('sum', function() {
		it('sum with a non-number first parameter, should return undefined', function() {
			expect(mathEnforcer.sum('o', 3)).to.be.equal(undefined, 'Function returns incorrect value!');
		});
		it('sum with a non-number second parameter, should return undefined', function() {
			expect(mathEnforcer.sum(5, 'ytt')).to.be.equal(undefined, 'Function returns incorrect value!');
		});
		it('sum with two non-number parameters, should return undefined', function() {
			expect(mathEnforcer.sum({ name 'pesho' }, new Date())).to.be.equal(undefined, 'Function returns incorrect value!');
		});
		it('sum with two negative numbers, should return correct value', function() {
			expect(mathEnforcer.sum(-5, -9)).to.be.equal(-14, 'Function returns correct value!');
		});
		it('sum with two positive numbers, should return correct value', function() {
			expect(mathEnforcer.sum(3, 8)).to.be.equal(11, 'Function returns correct value!');
		});
		it('sum with two floating-point numbers, should return correct value', function() {
			expect(mathEnforcer.sum(11.22, 53.79)).to.be.equal(65.01, 'Function returns correct value!');
		});
	});
});