function solve(input) {
  let namePattern = /\*[A-Z][a-zA-Z]*(?= |\t|$)/g;
  let phonePattern = /\+[0-9-]{10}(?= |\t|$)/g;
  let idPattern = /![a-zA-Z0-9]+(?= |\t|$)/g;
  let basePattern = /_[a-zA-Z0-9]+(?= |\t|$)/g;
  for (let i = 0; i < input.length; i++) {
    console.log(input[i]
      .replace(namePattern, x => '|'.repeat(x.length))
      .replace(phonePattern, x => '|'.repeat(x.length))
      .replace(idPattern, x => '|'.repeat(x.length))
      .replace(basePattern, x => '|'.repeat(x.length)));
  }
}