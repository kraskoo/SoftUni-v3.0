describe('sharedObject', function() {
	before(() => global.$ = $);
	describe('initially name and income should be null', function() {
		it('should return null for initial state of name', function() {
			expect(sharedObject.name).to.be.null;
		});
		it('should return null for initial state of income', function() {
			expect(sharedObject.income).to.be.null;
		});
	});
	describe('sharedObject.changeName', function() {
		it('value empty string, should not change name', function() {
			sharedObject.changeName('');
			expect(sharedObject.name).to.be.null;
			expect($('#name').val()).to.be.equal('');
		});
		it('new value, should change name', function() {
			sharedObject.changeName('pesho');
			expect(sharedObject.name).to.be.equal('pesho');
			expect($('#name').val()).to.be.equal('pesho');
		});
	});
	describe('sharedObject.changeIncome', function() {
		it('string value, should not change income', function() {
			sharedObject.changeIncome('pesho');
			expect(sharedObject.income).to.be.null;
			expect($('#income').val()).to.be.equal('');
		});
		it('floating-point number value, should not change income', function() {
			sharedObject.changeIncome(2.33);
			expect(sharedObject.income).to.be.null;
			expect($('#income').val()).to.be.equal('');
		});
		it('negative number value, should not change income', function() {
			sharedObject.changeIncome(-1);
			expect(sharedObject.income).to.be.null;
			expect($('#income').val()).to.be.equal('');
		});
		it('zero number value, should not change income', function() {
			sharedObject.changeIncome(0);
			expect(sharedObject.income).to.be.null;
			expect($('#income').val()).to.be.equal('');
		});
		it('new value, should change income', function() {
			sharedObject.changeIncome(1);
			expect(sharedObject.income).to.be.equal(1);
			expect($('#income').val()).to.be.equal('1');
		});
	});
	describe('sharedObject.updateName', function() {
		it('value with empty string, should not update name', function() {
			sharedObject.changeName('pesho');
			$('#name').val('');
			sharedObject.updateName();
			expect(sharedObject.name).to.be.equal('pesho');
			expect($('#name').val()).to.be.equal('');
		});
		it('floating-point value, should update name', function() {
			sharedObject.changeName('pesho');
			$('#name').val(5.32);
			sharedObject.updateName();
			expect(sharedObject.name).to.be.equal('5.32');
			expect($('#name').val()).to.be.equal('5.32');
		});
	});
	describe('sharedObject.updateIncome', function() {
		it('not a number value, should not update income', function() {
			sharedObject.changeIncome(4);
			$('#income').val('pesho');
			sharedObject.updateIncome();
			expect(sharedObject.income).to.be.equal(4);
			expect($('#income').val()).to.be.equal('pesho');
		});
		it('floating-point number value, should not update income', function() {
			sharedObject.changeIncome(9);
			$('#income').val(7.44);
			sharedObject.updateIncome();			
			expect(sharedObject.income).to.be.equal(9);
			expect($('#income').val()).to.be.equal('7.44');
		});
		it('negative number value, should not update income', function() {
			sharedObject.changeIncome(5);
			$('#income').val(-20);
			sharedObject.updateIncome();			
			expect(sharedObject.income).to.be.equal(5);
			expect($('#income').val()).to.be.equal('-20');
		});
		it('zero number value, should not update income', function() {
			sharedObject.changeIncome(10);
			$('#income').val(0);
			sharedObject.updateIncome();			
			expect(sharedObject.income).to.be.equal(10);
			expect($('#income').val()).to.be.equal('0');
		});
		it('new value, should update income', function() {
			sharedObject.changeIncome(2);
			$('#income').val(4);
			sharedObject.updateIncome();			
			expect(sharedObject.income).to.be.equal(4);
			expect($('#income').val()).to.be.equal('4');
		});
	});
});