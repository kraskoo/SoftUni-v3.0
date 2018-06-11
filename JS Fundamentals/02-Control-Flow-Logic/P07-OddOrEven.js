function solve(num) {
    var hasFraction = (num - parseInt(num)) !== 0;
    if (hasFraction) {
        console.log("invalid");
    } else if (num % 2 === 0) {
        console.log("even");
    } else if (num % 2 !== 0) {
        console.log("odd");
    }
}