function solve(input) {
  let result = input[0];
  for (let i = 1; i < input.length; i++) {
    result = result.concat(input[i]);
  }

  console.log(result.split('').reverse().join(''));
}

solve(['I', 'am', 'student']);