function solve(num) {
    if (num === 2) {
        console.log("+++");
        return;
    }

    var partLine = parseInt((num - 3) / 2);
    if (num <= 4) {
        partLine = 0;
    }

    var str = `+${'-'.repeat(num - 2)}+${'-'.repeat(num - 2)}+\n`;
    for (var i = 0; i < partLine; i++) {
        str += `|${' '.repeat(num - 2)}|${' '.repeat(num - 2)}|\n`;
    }

    str += `+${'-'.repeat(num - 2)}+${'-'.repeat(num - 2)}+\n`;
    for (var i = 0; i < partLine; i++) {
        str += `|${' '.repeat(num - 2)}|${' '.repeat(num - 2)}|\n`;
    }

    str += `+${'-'.repeat(num - 2)}+${'-'.repeat(num - 2)}+`;
    console.log(str);
}