function solve(input) {
  let current = 0;
  let array = [];
  for (let operation of input) {
    switch (operation) {
      case "add":
        array.push(++current);
        break;
      case "remove":
        ++current;
        array.pop();
        break;
    }
  }

  if (array.length === 0) {
    console.log("Empty");
  } else {
    array.map(el => console.log(el));
  }
}