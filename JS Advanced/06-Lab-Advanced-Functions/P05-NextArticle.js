function getArticleGenerator(articles) {
	let content = $('#content');

	return function () {
		if (articles.length > 0) {
			let article = $('<article>');
			article.append($(`<p>${articles.shift()}</p>`));
			content.append(article);
		}
	}
}