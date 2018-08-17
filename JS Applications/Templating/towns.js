function attachEvents() {
	$('#btnLoadTowns').on('click', function() {
		let towns = $('#towns').val().split(', ').map(x => ({ name: x }));
		let root = $('#root');
		root.empty();
		root.append($(Handlebars.compile($('#towns-template').html())({ towns: towns })));
	});
}