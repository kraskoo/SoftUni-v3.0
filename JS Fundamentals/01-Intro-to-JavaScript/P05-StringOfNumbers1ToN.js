function solve(num) {
    var endNumber = Number(num);
    var str = '';
    for(var i = 1; i <= endNumber; i++) {
        str += (i + '');
    }

    console.log(str);
}