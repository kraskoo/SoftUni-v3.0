describe('nuke', function() {
	before(() => global.$ = $);
	beforeEach(() => $(document.body).html(
		`<div id="target">
			<div class="nested target">
				<p>This is some text</p>
			</div>
			<div class="target">
				<p>Empty div</p>
			</div>
			<div class="inside">
				<span class="nested">Some more text</span>
				<span class="target">Some more text</span>
			</div>
		</div>`));
	it("should remove two divs for ('div', '.target')", function () {
		let initialTargetLength = $('.target').length;
		let initialDivLength = $('div').length;
		let initialDivTargetLength = $('.target').filter('div').length;
		nuke('div', '.target');
		expect($('.target').filter('div').length).to.be.equal(0);
		expect($('div').length).to.equal(initialDivLength - initialDivTargetLength);
		expect($('.target').length).to.equal(initialTargetLength - initialDivTargetLength);
	});
	it('should do nothing if selectors are same', function() {
		let html = $('body').html();
		nuke('div', 'div');
		expect(html).to.be.equal($('body').html());
	});
	it('should remove tag with existing attribute parameter', function() {
		let html = $('body').html();
		nuke('span', '.target');
		expect(html).to.be.not.equal($('body').html());
	});
	it('should do noting if the first selector does not exist', function() {
		let html = $('body').html();
		nuke('.pesho', 'div');
		expect(html).to.be.equal($('body').html());
	});
	it("should remove one span for", function () {
		nuke('.nested', 'span');
		expect($('span.nested').length).to.be.equal(0);
	});
});