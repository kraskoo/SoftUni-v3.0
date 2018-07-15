function solve(input, sortCriteria) {
	class Ticket {
		constructor(destination, price, status) {
			this.destination = destination;
			this.price = price;
			this.status = status;
		}
	}
	
	for (let i = 0; i < input.length; i++) {
		let tokens = input[i].split('|');
		input[i] = new Ticket(tokens[0], Number(tokens[1]), tokens[2]);
	}
	
	return input.sort((a, b) => {
		if (typeof a[sortCriteria] === 'string') {
			return a[sortCriteria].localeCompare(b[sortCriteria]);
		}
		
		return a[sortCriteria] - b[sortCriteria];
	});
}