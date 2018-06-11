function solve(text) {
  let pattern = /\(.+?\)/g;
  let matches = text.toString().match(pattern);
  if (matches === null) {
    return;
  }

  let matchArray = [];
  for (let i = 0; i < matches.length; i++) {
    matchArray.push(matches[i].substring(1, matches[i].length - 1));
  }

  console.log(matchArray.join(", "));
}

solve('Rakiya Bulgarian brandy is self-made liquor alcoholic drink');