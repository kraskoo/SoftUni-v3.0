function solve(input) {
  String.prototype.htmlEscape = function() {
    return this.replace(/&/g, "&amp;")
      .replace(/</g, "&lt;")
      .replace(/>/g, "&gt;")
      .replace(/"/g, "&quot;")
      .replace(/'/g, "&#39;");
  };

  let result = "<table>\n";
  for (let str of input) {
    let data = JSON.parse(str);
    result += "    <tr>\n";
    result += `        <td>${data.name.htmlEscape()}</td>\n`;
    result += `        <td>${data.position.htmlEscape()}</td>\n`;
    result += `        <td>${data.salary}</td>\n`;
    result += "    <tr>\n";
  }

  result += "</table>";
  console.log(result);
}