function solve(num) {
    var str = '<div class="chessboard">\n';
    for (var i = 1; i <= num; i++) {
        str += "  <div>\n"
        for(var j = 1; j <= num; j++) {
            str += `    <span class="${((i + j) % 2 === 0 ? "black" : "white")}"></span>\n`;
        }

        str += "  </div>\n"
    }

    str += "</div>";
    console.log(str);
}