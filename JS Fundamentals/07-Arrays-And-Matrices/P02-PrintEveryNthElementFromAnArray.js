function solve(input) {
  let n = input[input.length - 1];
  input.slice(0, input.length - 1).filter((el, idx) => idx % n === 0).map(el => console.log(el));
}