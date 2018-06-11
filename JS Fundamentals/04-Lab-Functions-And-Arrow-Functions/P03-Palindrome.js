function solve(word) {
  let reversed = "";
  for (let i = word.length - 1; i >= 0; i--) {
    reversed += word[i];
  }

  console.log(`${(word === reversed)}`);
}