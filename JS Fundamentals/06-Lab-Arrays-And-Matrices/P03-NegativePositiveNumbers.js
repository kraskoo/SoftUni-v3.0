function solve(input) {
  let newArray = [];
  for (let num of input) {
    if (num < 0) {
      newArray.unshift(num);
    } else {
      newArray.push(num);
    }
  }

  newArray.forEach(el => console.log(el));
}