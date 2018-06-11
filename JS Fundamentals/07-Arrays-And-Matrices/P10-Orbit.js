function solve(input) {
  let [width, height, x, y] = [input[0], input[1], input[2], input[3]];
  let matrix = [];
  for (let i = 0; i < width; i++) {
    matrix.push([]);
  }

  for (let i = 0; i < width; i++) {
    for (let j = 0; j < height; j++) {
      matrix[i][j] = Math.max(Math.abs(i - x), Math.abs(j - y)) + 1;
    }
  }

  console.log(matrix.map(r => r.join(' ')).join('\n'));
}