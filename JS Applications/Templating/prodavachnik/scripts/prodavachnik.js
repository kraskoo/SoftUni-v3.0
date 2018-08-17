function startApp() {
	let requester = (function () {
		const baseUrl = 'https://baas.kinvey.com/';
		const appKey = 'kid_BJ3bQL2N7';
		const appSecrete = '39737fc531ed45a29076377a7845e568';
		
		function makeAuth(type) {
			if (type === 'basic') {
				return `Basic ${btoa(appKey + ':' + appSecrete)}`;
			}
			
			return `Kinvey ${sessionStorage.getItem('authtoken')}`;
		}
		
		function getRequestObject(method, module, url, auth) {
			return {
				method,
				url: `${baseUrl}${module}/${appKey}/${url}`,
				headers: { 'Authorization': makeAuth(auth) }
			}
		}
		
		function get(module, url, auth) {
			return $.ajax(getRequestObject('GET', module, url, auth));
		}
		
		function post(module, url, auth, data) {
			let request = getRequestObject('POST', module, url, auth);
			request.data = data;
			return $.ajax(request);
		}
		
		function update(module, url, auth, data) {
			let request = getRequestObject('PUT', module, url, auth);
			request.data = data;
			return $.ajax(request);
		}
		
		function remove(module, url, auth) {
			return $.ajax(getRequestObject('DELETE', module, url, auth));
		}
		
		return { get, post, update, remove };
	}());
	$(document).on({
		ajaxStart: function() { $('#loadingBox').show(); },
		ajaxStop: function() { $('#loadingBox').hide(); }
	});
	$("#infoBox, #errorBox").on('click', function() {
		$(this).fadeOut();
	});
	showMenuNavigation();
	showView('home');
	$('#linkHome').on('click', () => { showView('home'); });
	$('#linkLogin').on('click', () => { showView('login'); });
	$('#linkRegister').on('click', () => { showView('register'); });
	$('#linkCreateAd').on('click', () => { showView('create'); });
	$('#linkListAds').on('click', showAds);
	$('#linkLogout').on('click', logout);
	$('#buttonRegisterUser').on('click', register);
	$('#buttonLoginUser').on('click', login);
	$('#buttonCreateAd').on('click', createAd);
	$('#buttonEditAd').on('click', editAd);
	
	function saveSession(info) {
		sessionStorage.setItem('username', info.username);
		sessionStorage.setItem('id', info._id);
		sessionStorage.setItem('authtoken', info._kmd.authtoken);
	}
	
	async function register() {
		let regForm = $('#formRegister');
		let username = regForm.find('input[name="username"]').val();
		let password = regForm.find('input[name="passwd"]').val();
		try {
			saveSession(await requester.post('user', '', 'basic', { username, password }));
			showMenuNavigation();
			showAds();
			showInfo('Successfully registred.');
			regForm.trigger('reset');
		} catch(error) {
			handleError(error);
		}
	}
	
	async function login() {
		let loginForm = $('#formLogin');
		let username = loginForm.find('input[name="username"]').val();
		let password = loginForm.find('input[name="passwd"]').val();
		try {
			saveSession(await requester.post('user', 'login', 'basic', { username, password }));
			showMenuNavigation();
			showAds();
			showInfo('Successfully logged in.');
			loginForm.trigger('reset');
		} catch(error) {
			handleError(error);
		}
	}
	
	async function logout() {
		try {
			await requester.post('user', '_logout', '', { 'authtoken': sessionStorage.getItem('authtoken') });
			sessionStorage.clear();
			showView('home');
			showMenuNavigation();
			showInfo('Log out.');
		} catch(error) {
			handleError(error);
		}
	}
	
	async function listAds() {
		try {
			let response = await requester.get('appdata', 'advertisements', '');
			let ads = $('#ads');
			let table = ads.find('table > tbody');
			table.empty();
			for (let advert of response) {
				let tr = $('<tr>').attr('data-id', advert._id);
				$('<td>').text(advert.Title).appendTo(tr);
				$('<td>').text(advert.Publisher).appendTo(tr);
				$('<td>').text(advert.Description).appendTo(tr);
				$('<td>').text(advert.Price).appendTo(tr);
				$('<td>').text(advert.DateOfPublishing).appendTo(tr);
				let specialTd = $('<td>');
				$('<a href="#">[View]</a>').on('click', detailAd).appendTo(specialTd);
				specialTd.appendTo(tr);
				if (advert._acl.creator === sessionStorage.getItem('id')) {
					$('<a href="#">[Edit]</a>').on('click', edit).appendTo(specialTd);
					$('<a href="#">[Delete]</a>').on('click', deleteAd).appendTo(specialTd);
				}
				
				tr.appendTo(table);
			}
		} catch(error) {
			handleError(error);
		}
	}
	
	async function detailAd() {
		let currentRow = $(this).parent().parent();
		let id = currentRow.attr('data-id');
		try {
			let response = await requester.get('appdata', `advertisements/${id}`, '');
			let section = $('#viewDetailsAd');
			section.find('#detailImage').attr('src', response.ImageUrl);
			section.find('#detailTitle').text(`Title: ${response.Title}`);
			section.find('#detailDescription').text(`Description: ${response.Description}`);
			section.find('#detailPrice').text(`Price: ${response.Price}`);
			section.find('#detailDateAndAuthor').text(`${response.DateOfPublishing} by ${response.Publisher}`);
			showView('details');
		} catch(error) {
			handleError(error);
		}
	}
	
	async function createAd() {
		let createForm = $('#formCreateAd');
		let title = createForm.find('input[name="title"]').val();
		let description = createForm.find('textArea[name="description"]').val();
		let price = Number(createForm.find('input[name="price"]').val());
		let imageUrl = createForm.find('input[name="imageUrl"]').val();
		let thisDate = new Date();
		let month = thisDate.getMonth() + 1;
		let day = thisDate.getDate();
		let datePublished = `${thisDate.getFullYear()}-${month < 10 ? '0' + month : month}-${day < 10 ? '0' + day : day}`;
		try {
			let response = await requester.post('appdata', 'advertisements', '', {
				'Title': title,
				'Description': description,
				'DateOfPublishing': datePublished,
				'Price': price,
				'Publisher': sessionStorage.getItem('username'),
				'ImageUrl': imageUrl
			});
			showAds();
			createForm.trigger('reset');
			showInfo('Successfully create ad');
		} catch(error) {
			handleError(error);
		}
	}
	
	async function editAd() {
		let editForm = $('#formEditAd');
		let id = editForm.find('input[name="id"]').val();
		let publisher = editForm.find('input[name="publisher"]').val();
		let title = editForm.find('input[name="title"]').val();
		let description = editForm.find('textarea[name="description"]').val();
		let datePublished = editForm.find('input[name="datePublished"]').val();
		let imageUrl = editForm.find('input[name="imageUrl"]').val();
		let price = editForm.find('input[name="price"]').val();
		try {
			let response = await requester.update('appdata', `advertisements/${id}`, '', {
				'Title': title,
				'Description': description,
				'ImageUrl': imageUrl,
				'DateOfPublishing': datePublished,
				'Price': price,
				'Publisher': publisher
			});
			showAds();
			showInfo(`Edited Ad ${id}`);
		} catch(error) {
			handleError(error);
		}
	}
	
	async function edit() {
		let currentRow = $(this).parent().parent();
		let id = currentRow.attr('data-id');
		try {
			let response = await requester.get('appdata', `advertisements/${id}`, '');
			let editForm = $('#formEditAd');
			editForm.trigger('reset');
			editForm.find('input[name="id"]').val(id);
			editForm.find('input[name="publisher"]').val(response.Publisher);
			editForm.find('input[name="title"]').val(response.Title);
			editForm.find('textarea[name="description"]').val(response.Description);
			editForm.find('input[name="datePublished"]').val(response.DateOfPublishing);
			editForm.find('input[name="imageUrl"]').val(response.ImageUrl);
			editForm.find('input[name="price"]').val(response.Price);
			showView('edit');
		} catch(error) {
			handleError(error);
		}
	}
	
	async function deleteAd() {
		let currentRow = $(this).parent().parent();
		id = currentRow.attr('data-id');
		try {
			let response = await requester.remove('appdata', `advertisements/${id}`, '');
			showAds();
			showInfo(`Deleted Ad ${id}`);
		} catch(error) {
			handleError(error);
		}
	}
	
	function showMenuNavigation() {
		// Have a logged in user
		if (sessionStorage.getItem('authtoken') !== null) {
			$('#linkHome').show();
			$('#linkListAds').show();
			$('#linkCreateAd').show();
			$('#linkLogout').show();
			$('#linkLogin').hide();
			$('#linkRegister').hide();
			$('#loggedInUser').text(`Welcome ${sessionStorage.getItem('username')}!`);
			$('#loggedInUser').show();
		} else {			
			$('#linkHome').show();
			$('#linkListAds').hide();
			$('#linkCreateAd').hide();
			$('#linkLogout').hide();
			$('#linkLogin').show();
			$('#linkRegister').show();
			$('#loggedInUser').text('');
			$('#loggedInUser').hide();
		}
	}
	
	function showAds() {
		listAds();
		showView('ads');
	}
	
	function showView(view) {
		$('section').hide();
		switch (view) {
			case 'home':
				$('#viewHome').show();				
				break;
			case 'register':
				$('#viewRegister').show();
				break;
			case 'login':
				$('#viewLogin').show();
				break;
			case 'create':
				$('#viewCreateAd').show();
				break;
			case 'edit':
				$('#viewEditAd').show();
				break;
			case 'ads':
				$('#viewAds').show();
				break;
			case 'details':
				$('#viewDetailsAd').show();
				break;
		}
	}
	
	function showError(msg) {
		let error = $('#errorBox');
		error.text(msg);
		error.show();
	}
	
	function showInfo(msg) {
		let info = $('#infoBox');
		info.text(msg);
		info.show();
		setTimeout(() => { info.fadeOut(); }, 2000);
	}
	
	function handleError(reason) {
		showError(reason.responseJSON.description);
	}
}