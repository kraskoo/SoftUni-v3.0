function sortArray(array, sortMethod) {
	let ascendingOrder = function(a, b) {
		return a - b;
	}
	
	let descendingOrder = function(a, b) {
		return b - a;
	}
	
	let sortingStrategies = {
		'asc': ascendingOrder,
		'desc': descendingOrder
	};
	
	return array.sort(sortingStrategies[sortMethod]);
}