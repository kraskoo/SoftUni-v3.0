function extractText() {
	let listTexts = $('#items > li').toArray().map(x => $(x).text());
	$('#result').append(listTexts.join(', '));
}