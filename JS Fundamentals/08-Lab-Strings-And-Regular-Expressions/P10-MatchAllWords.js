function solve(input) {
  console.log(input.split(/\W+/g).filter(x => x !== "").join('|'));
}