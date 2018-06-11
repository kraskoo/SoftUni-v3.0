function solve(input) {
  let set = new Set();
  let words = input.join(' ').split(/\W+/).filter(x => x !== '').map(x => x.toLowerCase());
  for (let word of words) {
    set.add(word);
  }

  console.log(Array.from(set).join(', '));
}