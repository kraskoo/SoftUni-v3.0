function solve(searchedWord, text) {
  let counter = 0;
  for (let i = 0; i < text.length; i++) {
    if (text.substring(i).startsWith(searchedWord)) {
      counter++;
    }
  }

  console.log(counter);
}