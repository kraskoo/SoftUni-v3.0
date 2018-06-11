function solve(arr) {
    [num, precision] = arr;
    if (precision > 15) {
        precision = 15;
    }

    console.log(Number(num.toFixed(precision)));
}