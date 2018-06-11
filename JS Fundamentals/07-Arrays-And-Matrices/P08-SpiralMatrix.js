function solve(rows, cols) {
  let [startRow, startCol, endRow, endCol] = [0, 0, rows - 1, cols - 1];
  let matrix = [];
  let counter = 0;
  for (let i = 0; i < rows; i++) {
    matrix.push([]);
  }

  while (startRow <= endRow && startCol <= endCol) {
    for (let i = startCol; i <= endCol; i++) {
      matrix[startRow][i] = ++counter;
    }

    for (let i = startRow + 1; i <= endRow; i++) {
      matrix[i][endCol] = ++counter;
    }

    for (let i = endCol - 1; i >= startCol; i--) {
      matrix[endRow][i] = ++counter;
    }

    for (let i = endRow - 1; i > startRow; i--) {
      matrix[i][startCol] = ++counter;
    }

    startRow++;
    startCol++;
    endRow--;
    endCol--;
  }

  console.log(matrix.map(r => r.join(" ")).join("\n"));
}