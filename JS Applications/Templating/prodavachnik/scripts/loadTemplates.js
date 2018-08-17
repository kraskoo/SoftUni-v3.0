$(() => {
	async function appendTemplateTo(path, id, respondObject) {
		let result = await $.ajax({
			method: 'GET',
			url: path
		});
		$(`#${id}`).prepend(respondObject ? Handlebars.compile(result)(respondObject) : Handlebars.compile(result)());
	}
	
	appendTemplateTo('./templates/regAndLoginForm.hbs', 'formLogin');
	appendTemplateTo('./templates/regAndLoginForm.hbs', 'formRegister');
	appendTemplateTo('./templates/advertTable.hbs', 'ads');
	appendTemplateTo('./templates/createAndEditForm.hbs', 'formCreateAd');
	appendTemplateTo('./templates/createAndEditForm.hbs', 'formEditAd');
	appendTemplateTo('./templates/viewDetails.hbs', 'viewDetailsAd');
});