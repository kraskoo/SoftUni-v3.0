describe('isOddOrEven', function() {
	it('with a number parameter, should return undefined', function() {
		expect(isOddOrEven(13)).to.equal(undefined, 'Function did not return the correct result!');
	});
	it('with a object parameter, should return undefined', function() {
		expect(isOddOrEven({ name: 'pesho' })).to.equal(undefined, 'Function did not return the correct result!');
	});
	it('with an even length string, should return correct result', function() {
		assert.equal(isOddOrEven('roar'), 'even', 'Function returns correct value!');
	});
	it('with an odd length string, should return correct result', function() {
		expect(isOddOrEven('pesho')).to.be.equal('odd', 'Function returns correct value!');
	});
	it('with multiple consecutive checks, should return correct values', function() {
		expect(isOddOrEven('cat')).to.be.equal('odd', 'Function returns correct value!');
		expect(isOddOrEven('alabala')).to.be.equal('odd', 'Function returns correct value!');
		expect(isOddOrEven('it is even')).to.be.equal('even', 'Function returns correct value!');
	});
});