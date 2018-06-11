function solve(input) {
  let str = "<ul>\n";
  for (let i = 0; i < input.length; i++) {
    let currentStr = input[i];
    currentStr = currentStr.replace(/&/g, '&amp;');
    currentStr = currentStr.replace(/</g, '&lt;');
    currentStr = currentStr.replace(/>/g, '&gt;');
    currentStr = currentStr.replace(/"/g, '&quot;');
    str += `  <li>${currentStr}</li>\n`;
  }

  str += "</ul>";
  console.log(str);
}