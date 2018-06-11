function solve(input) {
  let max = Number.NEGATIVE_INFINITY;
  for (let array of input) {
    for (let num of array) {
      if (num > max) {
        max = num;
      }
    }
  }

  console.log(max);
}