function solve(num) {
    for(var i = 1; i <= num; i++) {
        var str = '';
        for(var j = 1; j <= i; j++) {
            str += '$';
        }

        console.log(str);
    }
}