function solve(num) {
  let str = "";
  for (let i = 0; i < num; i++) {
    str += `${"*".repeat(i + 1)}\n`;
  }

  for (let i = num - 1; i >= 0; i--) {
    str += `${"*".repeat(i)}\n`;
  }

  console.log(str);
}

solve(1);