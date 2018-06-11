function solve(input) {
  let numbers = [];
  let regex = /\d+/g;
  for (let i = 0; i < input.length; i++) {
    let match = regex.exec(input[i]);
    while (match) {
      numbers.push(match[0]);
      match = regex.exec(input[i]);
    }
  }

  console.log(numbers.join(' '));
}

solve(['123a456',
  '789b987',
  '654c321',
  '0']);