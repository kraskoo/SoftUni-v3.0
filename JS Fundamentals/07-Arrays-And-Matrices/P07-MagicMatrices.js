function solve(input) {
  let rowSums = [];
  let colSums = [];
  for (let i = 0; i < input.length; i++) {
    rowSums.push(0);
    for (let j = 0; j < input[i].length; j++) {
      rowSums[rowSums.length - 1] += input[i][j];
    }

    if (i > 0 && rowSums[i - 1] !== rowSums[i]) {
      console.log(false);
      return;
    }
  }

  for (let i = 0; i < input.length; i++) {
    colSums.push(0);
    for (let j = 0; j < input[i].length; j++) {
      if (j < input.length) {
        colSums[colSums.length - 1] += input[j][i];
      }
    }

    if (i > 0 && colSums[i - 1] !== colSums[i]) {
      console.log(false);
      return;
    }
  }

  let length = Math.min(rowSums.length, colSums.length);
  for (let i = 0; i < length; i++) {
    if (rowSums[i] !== colSums[i]) {
      console.log(false);
      return;
    }
  }

  console.log(true);
}

solve([[1, 0, 0],
  [0, 0, 1],
  [0, 1, 0]]
);