function solve(input) {
  let pattern = /^([A-Z][a-zA-Z]*) - ([1-9][0-9]*) - ([a-zA-Z0-9\- ]+)$/;
  for (let i = 0; i < input.length; i++) {
    let match = pattern.exec(input[i]);
    if (match) {
      console.log(`Name: ${match[1]}`);
      console.log(`Position: ${match[3]}`);
      console.log(`Salary: ${match[2]}`);
    }
  }
}