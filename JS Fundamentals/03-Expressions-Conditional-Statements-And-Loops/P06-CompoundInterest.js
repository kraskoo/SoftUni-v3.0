function solve(arr) {
    var p = arr[0];
    var i = arr[1];
    var n = arr[2];
    var t = arr[3];
    var f = p * (Math.pow((1 + (i / (100 * (12 / n)))), (12 / n) * t));
    console.log(f.toFixed(2));
}