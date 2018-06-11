function solve(num) {
    var str = '<table border="1">\n';
    str += "  <tr>"
    for(var i = 0; i <= num; i++) {
        if (i === 0) {
            str += "<th>x</th>";
        } else {
            str += `<th>${i}</th>`;
        }
    }

    str += "</tr>\n";
    for(var i = 1; i <= num; i++) {
        str += "  <tr>";
        for(var j = 0; j <= num; j++) {
            if (j === 0) {
                str += `<th>${i}</th>`;
            } else {
                str += `<td>${i * j}</td>`;
            }
        }

        str += "</tr>\n";
    }

    str += "</table>";
    return str;
}