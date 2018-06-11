function solve(input) {
  console.log(input.sort((a, b) => a - b).slice(0, 2).join(" "));
}

solve([3, 0, 10, 4, 7, 3]);