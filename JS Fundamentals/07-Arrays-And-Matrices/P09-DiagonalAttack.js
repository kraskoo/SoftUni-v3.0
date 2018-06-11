function solve(input) {
  let matrix = [];
  let diagonal1 = 0;
  let diagonal2 = 0;

  for (let i = 0; i < input.length; i++) {
    matrix.push(input[i].split(' ').map(Number));
  }

  for (let i = 0; i < matrix.length; i++) {
    diagonal1 += matrix[i][i];
    diagonal2 += matrix[i][matrix.length - 1 - i];
  }

  if (diagonal1 !== diagonal2) {
    console.log(matrix.map(r => r.join(' ')).join('\n'));
  } else {
    let newMatrix = [];
    for (let i = 0; i < matrix.length; i++) {
      newMatrix.push([]);
      for (let j = 0; j < matrix[i].length; j++) {
        newMatrix[i].push(diagonal1);
      }

      newMatrix[i][i] = matrix[i][i];
      newMatrix[i][matrix.length - 1 - i] = matrix[i][matrix.length - 1 - i];
    }

    console.log(newMatrix.map(r => r.join(' ')).join('\n'));
  }
}