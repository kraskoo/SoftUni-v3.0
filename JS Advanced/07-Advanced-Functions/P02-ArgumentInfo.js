function solve() {
	let counter = new Map();
	for (let arg of arguments) {
		console.log(typeof(arg) + ": " + arg);
		if (!counter.has(typeof(arg))) {
			counter.set(typeof(arg), 1);
		} else {
			counter.set(typeof(arg), counter.get(typeof arg) + 1)
		}
	}
	
	counter = ([...counter.entries()].sort((a, b) => b[1] - a[1]));
	for (let [k, v] of counter) {
		console.log(k + " = " + v);
	}
}