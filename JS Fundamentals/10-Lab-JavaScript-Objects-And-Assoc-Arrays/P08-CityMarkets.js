function solve(input) {
  let map = new Map();
  for (let row of input) {
    let [town, product, amount, price] = row.split(/ -> | : /);
    if (!map.has(town)) {
      map.set(town, new Map());
    }

    map.get(town).set(product, Number(price) * Number(amount));
  }

  for ([townKey, townValue] of map) {
    console.log(`Town - ${townKey}`);
    for ([productKey, productValue] of townValue) {
      console.log(`$$$${productKey} : ${productValue}`);
    }
  }
}

solve(['Sofia -> Laptops HP -> 200 : 2000',
'Sofia -> Raspberry -> 200000 : 1500',
'Sofia -> Audi Q7 -> 200 : 100000',
'Montana -> Portokals -> 200000 : 1',
'Montana -> Qgodas -> 20000 : 0.2',
'Montana -> Chereshas -> 1000 : 0.3']);