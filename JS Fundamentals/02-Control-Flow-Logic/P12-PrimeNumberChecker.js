function solve(num) {
    if (num === 1) {
        console.log(false);
        return;
    }

    var isPrime = true;
    for (var i = 2; i < 10; i++) {
        if (i !== num && num % i === 0) {
            isPrime = false;
        }
    }

    console.log(isPrime);
}