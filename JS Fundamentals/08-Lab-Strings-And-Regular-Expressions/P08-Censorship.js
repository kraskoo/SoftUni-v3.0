function solve(text, censorship) {
  for (let i = 0; i < censorship.length; i++) {
    let censor = "-".repeat(censorship[i].length);
    text = text.split(censorship[i]).join(censor);
  }

  console.log(text);
}