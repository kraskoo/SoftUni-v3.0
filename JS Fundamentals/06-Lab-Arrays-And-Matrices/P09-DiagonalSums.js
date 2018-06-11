function solve(input) {
  let diagonal1 = 0;
  let diagonal2 = 0;
  for (let i = 0; i < input.length; i++) {
    diagonal1 += input[i][i];
    diagonal2 += input[i][input[i].length - 1 - i];
  }

  console.log(`${diagonal1} ${diagonal2}`);
}