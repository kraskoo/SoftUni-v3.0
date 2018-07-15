let data = require('./data');
function sort(property) {
	return data.sort((a, b) => (a[property] + '').localeCompare(b[property] + ''));
}

function filter(property, value) {
	return data.filter(x => x[property] === value);
}

module.exports = { sort, filter };