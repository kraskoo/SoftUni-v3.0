function solve(num) {
  function sumNumbers(str) {
    let sum = 0;
    for (let c of str) {
      sum += Number(c);
    }

    return sum;
  }

  let numAsString = num.toString();
  let sum = sumNumbers(numAsString);
  while (sum / numAsString.length <= 5) {
    numAsString += "9";
    sum += 9;
  }

  console.log(numAsString);
}

solve(101);
solve(5835);