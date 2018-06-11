function solve(input) {
  let quantities = {};
  let bottles = {};
  for (let row of input) {
    let data = row.split(' => ');
    let fruit = data[0];
    let quantity = Number(data[1]);
    if (!quantities.hasOwnProperty(fruit)) {
      quantities[fruit] = 0;
    }

    quantities[fruit] += quantity;
    if (quantities[fruit] >= 1000) {
      bottles[fruit] = parseInt(quantities[fruit] / 1000);
    }
  }

  let keys = Object.keys(bottles);
  for (let key of keys) {
    console.log(`${key} => ${bottles[key]}`);
  }
}