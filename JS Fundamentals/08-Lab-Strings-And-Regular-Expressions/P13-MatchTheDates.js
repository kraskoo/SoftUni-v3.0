function solve(input) {
  let pattern = /\b(\d{1,2})-([A-Z]{1}[a-z]{2})-(\d{4})/g;
  for (let i = 0; i < input.length; i++) {
    while (match = pattern.exec(input[i])) {
      console.log(`${match[0]} (Day: ${match[1]}, Month: ${match[2]}, Year: ${match[3]})`);
    }
  }
}