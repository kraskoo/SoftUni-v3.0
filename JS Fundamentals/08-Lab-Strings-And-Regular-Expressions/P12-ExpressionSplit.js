function solve(input) {
  input.split(/[\s(),;.]/).filter(x => x !== "").forEach(x => console.log(x));
}