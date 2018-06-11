function solve(input) {
  function operateWithNumber(operation, number) {
    switch (operation) {
      case "chop": return number / 2;
      case "dice": return Math.sqrt(number);
      case "spice": return number + 1;
      case "bake": return number * 3;
      case "fillet": return (number - (number * 0.2));
    }
  }

  let number = Number(input[0]);
  for (let i = 1; i < input.length; i++) {
    number = operateWithNumber(input[i], number);
    console.log(number);
  }
}