function solve(input) {
  let length = input[0].length;
  let counter = 0;
  for (let i = 0; i < length; i++) {
    for (let j = 1; j < input.length; j++) {
      if (input[j - 1][i] === input[j][i]) {
        counter++;
      }
    }
  }

  for (let i = 0; i < input.length; i++) {
    for (let j = 1; j < input[i].length; j++) {
      if (input[i][j - 1] === input[i][j]) {
        counter++;
      }
    }
  }

  console.log(counter);
}

solve([['test', 'yes', 'yo', 'ho'],
  ['well', 'done', 'yo', '6'],
  ['not', 'done', 'yet', '5']]
);