function solve(input) {
  let evens = input.filter((el, idx) => idx % 2 === 0).join(" ");
  console.log(evens);
}