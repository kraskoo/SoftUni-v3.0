describe('lookupChar', function() {
	it('with a non-string first parameter, should return undefined', function() {
		expect(lookupChar(0, 10)).to.be.equal(undefined, 'Function did not return correct value!');
	});
	it('with a non-number second parameter, should return undefined', function() {
		expect(lookupChar('pesho', 'gosho')).to.be.equal(undefined, 'Function did not return correct value!');
	});
	it('with a floating-point number second parameter, should return undefined', function() {
		expect(lookupChar('pesho', 3.14)).to.be.equal(undefined, 'Function did not return correct value!');
	});
	it('with an incorrect index value, should return "Incorrect index"', function() {
		expect(lookupChar('pesho', 18)).to.be.equal('Incorrect index', 'Function did not return correct value!');
	});
	it('with a negative index value, should return "Incorrect index"', function() {
		expect(lookupChar('pesho', -1)).to.be.equal('Incorrect index', 'Function did not return correct value!');
	});
	it('with an index value equal to string length, should return "Incorrect index"', function() {
		expect(lookupChar('pesho', 5)).to.be.equal('Incorrect index', 'Function did not return correct value!');
	});
	it('with correct parametes, should return correct value', function() {
		expect(lookupChar('pesho', 0)).to.be.equal('p', 'Function returns correct value!');
	});
	it('with correct parametes, should return correct value', function() {
		expect(lookupChar('pesho', 4)).to.be.equal('o', 'Function returns correct value!');
	});
});