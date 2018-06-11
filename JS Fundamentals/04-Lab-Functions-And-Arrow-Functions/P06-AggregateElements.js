function solve(input) {
  let sum = function (arr) {
    let s = 0;
    for (let i = 0; i < arr.length; i++) {
      s += arr[i];
    }

    return s;
  };

  let reverseSum = function (arr) {
    let s = 0;
    for (let i = 0; i < arr.length; i++) {
      s += 1 / arr[i];
    }

    return s;
  };

  let concat = function (arr) {
    let str = "";
    for (let i = 0; i < arr.length; i++) {
      str += arr[i];
    }

    return str;
  };

  console.log(sum(input));
  console.log(reverseSum(input));
  console.log(concat(input));
}