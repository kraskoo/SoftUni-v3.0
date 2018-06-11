function solve(input) {
  let result = new Map();
  for (let row of input) {
    let data = row.split(' : ');
    let name = data[0];
    let price = Number(data[1]);
    if (!result.has(name)) {
      result.set(name, price);
    }
  }

  let letters = new Set();
  let sortedKeys = Array.from(result.keys()).sort();
  sortedKeys.forEach(x => letters.add(x[0]));
  for (let key of letters) {
    console.log(key);
    for (let product of sortedKeys) {
      if (product.startsWith(key)) {
        console.log(`  ${product}: ${result.get(product)}`);
      }
    }
  }
}