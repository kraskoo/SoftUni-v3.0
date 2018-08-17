$(() => {
	renderCatTemplate();
	function renderCatTemplate() {
		$('#allCats').append($(Handlebars.compile($('#cat-template').html())({ cats: window.cats })));
		$('.card-block > button').each((i, e) => {
			$(e).on('click', function() {
				let currentContainer = $(this).parent().find('div');
				currentContainer.css('display', currentContainer.css('display') === 'none' ? 'block' : 'none');
			});
		});
	}
})