function solve(sentence) {
  let words = sentence.split(/\W+/);
  let upperCaseWords = [];
  for (let i = 0; i < words.length; i++) {
    if (words[i] !== "") {
      upperCaseWords.push(words[i].toUpperCase());
    }
  }

  console.log(upperCaseWords.join(", "));
}