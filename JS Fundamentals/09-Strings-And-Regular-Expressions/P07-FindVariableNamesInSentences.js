function solve(input) {
  let variables = [];
  let pattern = /\b_([a-zA-Z0-9]+)\b/g;
  let match = pattern.exec(input);
  while (match) {
    variables.push(match[1]);
    match = pattern.exec(input);
  }

  console.log(variables.join(','));
}