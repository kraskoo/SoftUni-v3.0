function solve(input) {
  String.prototype.htmlEscape = function () {
    return this.replace(/&/g, "&amp;")
      .replace(/</g, "&lt;")
      .replace(/>/g, "&gt;")
      .replace(/"/g, "&quot;")
      .replace(/'/g, "&#39;");
  };

  let jsonArray = JSON.parse(input);
  let result = "<table>\n";
  result += "  <tr><th>name</th><th>score</th></tr>\n";
  for (const nameAndScore of jsonArray) {
    result += `  <tr><td>${nameAndScore.name.htmlEscape()}</td><td>${nameAndScore.score}</td></tr>\n`;
  }

  result += "</table>";
  console.log(result);
}

solve('[{"name":"Pesho & Kiro","score":479},{"name":"Gosho, Maria & Viki","score":205}]');