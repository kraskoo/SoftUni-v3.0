function solve(input) {
  let array = input.filter((el, idx) => idx % 2 !== 0).map(el => el * 2).reverse();
  console.log(array.join(' '));
}

solve([10, 15, 20, 25]);
solve([3, 0, 10, 4, 7, 3]);