function solve(num) {
    var str = "<ul>\n";
    for (var i = 1; i <= num; i++) {
        str += `  <li><span style='color:${(i % 2 === 0 ? 'blue' : 'green')}'>${i}</span></li>\n`;
    }

    str += "</ul>";
    console.log(str);
}