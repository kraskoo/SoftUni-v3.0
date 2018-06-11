function solve(word, letter) {
    var count = 0;
    for (var l in word) {
        if (word[l] === letter) {
            count++;
        }
    }

    console.log(count);
}