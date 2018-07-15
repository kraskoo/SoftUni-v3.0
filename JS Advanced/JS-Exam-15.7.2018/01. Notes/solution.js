function addSticker() {
	let title = $('.title');
	let content = $('.content');
	let stickerList = $('#sticker-list');
	if (title.val() !== '' && content.val() !== '') {
		stickerList.append($('<li class="note-content">')
			.append($('<a class="button">').text('x').on('click', close))
			.append($('<h2>').text(title.val()))
			.append($('<hr>'))
			.append($('<p>').text(content.val())));
		title.val('');
		content.val('');
	}
	
	function close() {
		$(this).parent().remove();
	}
}