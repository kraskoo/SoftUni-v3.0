function solve(input) {
  String.prototype.htmlEscape = function () {
    return this.replace(/&/g, "&amp;")
      .replace(/</g, "&lt;")
      .replace(/>/g, "&gt;")
      .replace(/"/g, "&quot;")
      .replace(/'/g, "&#39;");
  };

  let array = JSON.parse(input);
  let output = "<table>\n";
  output += "  <tr>";
  Object.keys(array[0]).forEach(x => {
    output += `<th>${x}</th>`
  });
  output += "</tr>\n";
  for (const object of array) {
    output += "  <tr>";
    for (const key in object) {
      output += `<td>${object[key].toString().htmlEscape()}</td>`;
    }

    output += "</tr>\n";
  }

  output += "</table>";
  console.log(output);
}

solve('[{"Name":"Tomatoes & Chips","Price":2.35},{"Name":"J&B Chocolate","Price":0.96}]');