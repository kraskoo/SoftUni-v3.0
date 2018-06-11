function solve(input) {
  let map = new Map();
  for (let row of input) {
    let [town, product, price] = row.split(/ \| /);
    if (!map.has(product)) {
      map.set(product, new Map());
    }

    map.get(product).set(town, Number(price));
  }

  for (let [productKey, productValue] of map) {
    let keys = Array.from(productValue.keys()).sort((a, b) => {
      return productValue.get(a) - productValue.get(b);
    });

    console.log(`${productKey} -> ${productValue.get(keys[0])} (${keys[0]})`);
  }
}

solve(['Sample Town | Sample Product | 1000',
  'Sample Town | Orange | 2',
  'Sample Town | Peach | 1',
  'Sofia | Orange | 3',
  'Sofia | Peach | 2',
  'New York | Sample Product | 1000.1',
  'New York | Burger | 10'
]);