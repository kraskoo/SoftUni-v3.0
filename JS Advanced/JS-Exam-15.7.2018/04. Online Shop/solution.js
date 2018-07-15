function onlineShop(selector) {
	let form = `<div id="header">Online Shop Inventory</div>
	<div class="block">
		<label class="field">Product details:</label>
		<br>
		<input placeholder="Enter product" class="custom-select">
		<input class="input1" id="price" type="number" min="1" max="999999" value="1"><label class="text">BGN</label>
		<input class="input1" id="quantity" type="number" min="1" value="1"><label class="text">Qty.</label>
		<button id="submit" class="button" disabled>Submit</button>
		<br><br>
		<label class="field">Inventory:</label>
		<br>
		<ul class="display">
		</ul>
		<br>
		<label class="field">Capacity:</label><input id="capacity" readonly>
		<label class="field">(maximum capacity is 150 items.)</label>
		<br>
		<label class="field">Price:</label><input id="sum" readonly>
		<label class="field">BGN</label>
	</div>`;
	$(selector).html(form);
	let currentQuantity = 0;
	let currentPrice = 0;
	$('.custom-select').on('input', function() {
		if ($(this).val() !== '') enableSubmit();
		else disableSubmit();
	});
	
	$('.button').on('click', function() {
		let newQuantity = $('#quantity').val();
		let newPrice = $('#price').val();
		currentQuantity += Number(newQuantity);
		currentPrice += Number(newPrice);
		let resultText = `Product: ${$('.custom-select').val()} Price: ${newPrice} Quantity: ${newQuantity}`;
		$('.display').append($('<li>').text(resultText));			
		$('#capacity').val(currentQuantity);
		$('#sum').val(currentPrice);
		resetFields();
		if (currentQuantity >= 150) {
			$('#capacity').addClass('fullCapacity');
			$('#capacity').val('full');
			$('.custom-select').attr('disabled', true);
			$('#price').attr('disabled', true);
			$('#quantity').attr('disabled', true);
		}		
	});
	
	function resetFields() {
		$('.custom-select').val('');
		$('#price').val(1);
		$('#quantity').val(1);
		disableSubmit();
	}
	
	function disableSubmit() {
		$('.button').attr('disabled', true);
	}
	
	function enableSubmit() {		
		$('.button').attr('disabled', false);
	}
}