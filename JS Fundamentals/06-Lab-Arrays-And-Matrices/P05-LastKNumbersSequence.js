function solve(n, k) {
  let array = [1];
  for (let i = 1; i < n; i++) {
    let start = Math.max(0, i - k);
    array.push(array.slice(start, start + k).reduce((a, b) => a + b));
  }

  console.log(array.join(" "))
}