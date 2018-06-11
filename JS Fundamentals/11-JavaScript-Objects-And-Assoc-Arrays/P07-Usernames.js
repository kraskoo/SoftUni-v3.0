function solve(input) {
  let result = new Set();
  for (let row of input) {
    result.add(row);
  }

  Array.from(result.keys()).sort((a, b) => {
    let sizeDiff = a.length - b.length;
    if (sizeDiff === 0) return a.localeCompare(b);
    else return sizeDiff;
  }).forEach(x => console.log(x));
}

solve([
  'Ashton',
  'Kutcher',
  'Ariel',
  'Lilly',
  'Keyden',
  'Aizen',
  'Billy',
  'Braston'
]);