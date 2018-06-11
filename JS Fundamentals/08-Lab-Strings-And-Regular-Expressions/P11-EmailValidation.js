function solve(input) {
  console.log(/^[a-zA-Z0-9]+?@[a-z]+\.[a-z]+$/.test(input) ? 'Valid' : 'Invalid');
}