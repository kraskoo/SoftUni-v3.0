function solve(input) {
  let newArray = [input[0]];
  input
    .slice(1, input.length)
    .filter(el => {
      let isBiggerOrEqual = el >= newArray[newArray.length - 1];
      if (isBiggerOrEqual) newArray.push(el);
      return isBiggerOrEqual;
    });
  newArray.map(el => console.log(el));
}