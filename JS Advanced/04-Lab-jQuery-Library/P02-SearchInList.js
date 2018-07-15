function search() {
	$(`#towns > li`).css('font-weight', '');
	$('#result').text('');
	let searchValue = $('#searchText').val();
	let lis = $(`#towns > li:contains('${searchValue}')`);
	lis.css('font-weight', 'bold');
	$('#result').text(`${lis.length} matches found.`);
}