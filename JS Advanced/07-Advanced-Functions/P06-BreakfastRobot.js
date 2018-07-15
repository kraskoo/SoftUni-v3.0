function solve() {
	let robot = {
		protein: 0,
		carbohydrate: 0,
		fat: 0,
		flavour: 0,
	};
	let products = {
		apple: {
			carbohydrate: 1,
			flavour: 2
		},
		coke: {
			carbohydrate: 10,
			flavour: 20
		},
		burger: {
			carbohydrate: 5,
			fat: 7,
			flavour: 3
		},
		omelet: {
			protein: 5,
			flavour: 1,
			fat: 1
		},
		cheverme: {
			protein: 10,
			carbohydrate: 10,
			fat: 10,
			flavour: 10
		}
	};

	return function (input) {
		let inputData = input.split(' ');
		let command = inputData[0];

		if (command === 'restock') {
			let microElement = inputData[1];
			let quantity = +inputData[2];
			robot[microElement] += quantity;
			return ('Success');
		} else if (command === 'report') {
			return (`protein=${robot.protein} carbohydrate=${robot.carbohydrate} fat=${robot.fat} flavour=${robot.flavour}`);
		} else if (command === 'prepare') {
			let selectedProduct = inputData[1];
			let selectedQuantity = +inputData[2];
			let currentProductStats = products[selectedProduct];
			let canBeCooked = true;

			for (let microElement in currentProductStats) {
				if (currentProductStats.hasOwnProperty(microElement)) {
					let elementQuantity = currentProductStats[microElement];
					if (robot[microElement] < elementQuantity * selectedQuantity) {
						canBeCooked = false;
						return ('Error: not enough ' + microElement + ' in stock');
					}
				}
			}
			
			if (canBeCooked) {
				for (let microElement in currentProductStats) {
					if (currentProductStats.hasOwnProperty(microElement)) {
						let elementQuantity = currentProductStats[microElement];
						robot[microElement] -= elementQuantity * selectedQuantity;
					}
				}
				
				return ('Success');
			}
		}
	}
}