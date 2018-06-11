function solve(sentence, word) {
  let pattern = new RegExp(`\\b${word}\\b`, "gi");
  let match = pattern.exec(sentence);
  let counter = 0;
  while (match) {
    counter++;
    match = pattern.exec(sentence);
  }

  console.log(counter);
}