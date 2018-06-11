function solve(input) {
  input
    .sort((a, b) => {
      if (a.length === b.length) return a.localeCompare(b);
      else return a.length - b.length;
    })
    .map(el => console.log(el));
}