function solve(input) {
  let links = [];
  let pattern = /www\.[a-zA-Z0-9-]+(\.[a-z]+)+/g;
  for (let i = 0; i < input.length; i++) {
    let match = pattern.exec(input[i]);
    while (match) {
      links.push(match[0]);
      match = pattern.exec(input[i]);
    }
  }

  console.log(links.join('\n'));
}