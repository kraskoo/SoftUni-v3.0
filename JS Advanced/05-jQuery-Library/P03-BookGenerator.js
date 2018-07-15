let create = (function () {
	let id = 1;
	return function(selector, title, author, isbn) {
		$(selector).append($('<div>').attr('id', `book${id++}`)
			.append($(`<p>${title}</p>`).addClass('title'))
			.append($(`<p>${author}</p>`).addClass('author'))
			.append($(`<p>${isbn}</p>`).addClass('isbn'))
			.append($(`<button>Select</button>`).on('click', select))
			.append($(`<button>Deselect</button>`).on('click', deselect)));
		function select() {
			$(this).parent().css('border', '2px solid blue');
		}
		
		function deselect() {
			$(this).parent().css('border', 'none');
		}
	}
}());
function createBook(selector, title, author, isbn) {
	create(selector, title, author, isbn);
}