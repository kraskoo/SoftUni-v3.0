function solve(input) {
  function isValidDistance(x1, y1, x2, y2) {
    return Number.isInteger(Math.sqrt(Math.pow(x1 - x2, 2) + Math.pow(y1 - y2, 2))) ? "valid" : "invalid";
  }

  let [x1, y1, x2, y2] = input;
  console.log(`{${x1}, ${y1}} to {0, 0} is ${isValidDistance(x1, y1, 0, 0)}`);
  console.log(`{${x2}, ${y2}} to {0, 0} is ${isValidDistance(x2, y2, 0, 0)}`);
  console.log(`{${x1}, ${y1}} to {${x2}, ${y2}} is ${isValidDistance(x1, y1, x2, y2)}`);
}