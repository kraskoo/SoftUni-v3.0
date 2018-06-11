function solve(input) {
  console.log(input.split(/\s+/g).map(x => x[0].toUpperCase() + x.substring(1).toLowerCase()).join(' '));
}