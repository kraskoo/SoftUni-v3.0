function solve(input) {
  let [x1, y1, x2, y2, x3, y3] = input;
  let distance = (x1, y1, x2, y2) => Math.sqrt(((x1 - x2) ** 2) + ((y1 - y2) ** 2));
  let firstDist = distance(x1, y1, x2, y2) + distance(x2, y2, x3, y3);
  let secondDist = distance(x1, y1, x3, y3) + distance(x3, y3, x2, y2);
  let thirdDist = distance(x2, y2, x1, y1) + distance(x1, y1, x3, y3);
  let shortestDist = Math.min(firstDist, secondDist, thirdDist);
  if (shortestDist === firstDist) {
    console.log(`1->2->3: ${shortestDist}`);
  } else if (shortestDist === secondDist) {
    console.log(`1->3->2: ${shortestDist}`);
  } else if (shortestDist === thirdDist) {
    console.log(`2->1->3: ${shortestDist}`);
  }
}

solve([5, 1, 1, 1, 5, 4]);