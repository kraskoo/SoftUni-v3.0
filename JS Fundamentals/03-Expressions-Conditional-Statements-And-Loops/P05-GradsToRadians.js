function solve(num) {
    var deg = num * 0.9;
    deg %= 360;
    if (deg < 0) {
        deg += 360;
    }

    console.log(deg);
}