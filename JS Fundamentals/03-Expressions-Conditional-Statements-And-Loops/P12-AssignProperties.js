function solve(arr) {
    var obj = {};
    for (var i = 0; i < arr.length; i += 2) {
        obj[arr[i]] = arr[i + 1];
    }

    console.log(obj);
}