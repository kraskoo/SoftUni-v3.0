function solve(input) {
  let result = new Map();
  for (let row of input) {
    let array = JSON.parse(row).map(Number).sort((a, b) => b - a);
    let key = JSON.stringify(array).replace(/,/g, ', ');
    if (!result.has(key)) {
      result.set(key, array.length);
    }
  }

  Array.from(result.keys()).sort((a, b) => result.get(a) - result.get(b)).forEach(x => console.log(x));
}

solve(['[-3, -2, -1, 0, 1, 2, 3, 4]',
  '[10, 1, -17, 0, 2, 13]',
  '[4, -3, 3, -2, 2, -1, 1, 0]'
]);