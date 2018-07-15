const Console = require('../Console');
const expect = require('chai').expect;
describe('Console.writeLine()', function() {
	it('should return value successfully for a string message', function() {
		expect(Console.writeLine('message')).to.be.equal('message');
	});
	it('should return successfully JSON.stringify message for an object value', function() {
		let object = { name: 'Pesho', age: 11 };
		expect(Console.writeLine(object)).to.be.equal(JSON.stringify(object));
	});
	it('should throw an error if arguments are missing', function() {
		expect(() => Console.writeLine()).to.throw(TypeError);
	});
	it('should throw an error if first argument is not a string', function() {
		expect(() => Console.writeLine(5, 3, 8)).to.throw(TypeError);
	});
	it('should throw an error if placeholders length is not equal to tokens length', function() {
		expect(() => Console.writeLine('{0} + {1} = {2}', 3, 5)).to.throw(RangeError);
	});
	it('should throw an error if tokens length is not equal to placeholders length', function() {
		expect(() => Console.writeLine('{0} + {1} = {2}', 3, 5, 8, 9)).to.throw(RangeError);
	});
	it('should throw an error if any placeholder have incorrect value', function() {
		expect(() => Console.writeLine('Some {0} {0}.', 'test', 'message')).to.throw(RangeError);
	});
	it('should throw an error if the placeholder have incorrect value', function() {
		expect(() => Console.writeLine('{11} is invalid', 'This message')).to.throw(RangeError);
	});
	it('should return successfully message with correct placeholders and tokens', function() {
		expect(Console.writeLine('Some {0} {1}.', 'test', 'message')).to.be.equal('Some test message.');
	});
});