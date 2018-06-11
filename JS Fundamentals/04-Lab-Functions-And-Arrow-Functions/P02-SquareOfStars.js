function solve(num) {
  let str = "";
  for (let i = 0; i < num; i++) {
    str += `${"* ".repeat(num)}\n`;
  }

  console.log(str);
}

solve(5);