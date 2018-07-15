function mapSort(map, sortFn) {
	if (sortFn) {
		let sortedKeys = Array.from(map.entries()).sort(sortFn);
		return new Map(sortedKeys);
	} else {
		let sortedKeys = Array.from(map.entries()).sort((a, b) => (a[0] + '').localeCompare(b[0] + ''));
		return new Map(sortedKeys);
	}
}

module.exports = mapSort;