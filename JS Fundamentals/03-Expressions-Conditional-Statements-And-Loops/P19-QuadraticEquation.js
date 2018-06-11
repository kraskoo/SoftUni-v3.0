function solve(a, b, c) {
    var d = Math.pow(b, 2) - (4 * a * c);
    if (d > 0) {
        var x1 = ((-b + Math.sqrt(d)) / (2 * a));
        var x2 = ((-b - Math.sqrt(d)) / (2 * a));
        if (x1 < x2) {
            console.log(x1);
            console.log(x2);
        } else if (x1 > x2) {
            console.log(x2);
            console.log(x1);
        } else {
            console.log(x1);
            console.log(x2);
        }
    } else if (d === 0) {
        var x = -b / (2 * a);
        console.log(x);
    } else if (d < 0) {
        console.log("No");
    }
}