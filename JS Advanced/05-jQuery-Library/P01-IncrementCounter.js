function increment(selector) {
	let container = $(selector);
	let fragment = document.createDocumentFragment();
	let textArea = $('<textarea>').addClass('counter').attr('disabled', true).val(0);
	let incrementBtn = $('<button>Increment</button>').addClass('btn').attr('id', 'incrementBtn').on('click', function() {
		textArea.val(+textArea.val() + 1);
	});
	let list = $('<ul></ul>').addClass('results');
	let add = $('<button>Add</button>').addClass('btn').attr('id', 'addBtn').on('click', function() {
		$(`<li>${textArea.val()}</li>`).appendTo(list);
	});
	textArea.appendTo(fragment);
	incrementBtn.appendTo(fragment);
	add.appendTo(fragment);
	list.appendTo(fragment);
	container.append(fragment);
}