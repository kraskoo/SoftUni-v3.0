function solve() {
    var arr = arguments[0];
    var sum = 0;
    for (var el in arr) {
        sum += Number(arr[el]);
    }

    var vat = sum * 0.2;
    var total = sum + vat;
    console.log("sum = " + sum);
    console.log("VAT = " + vat);
    console.log("total = " + total);
}