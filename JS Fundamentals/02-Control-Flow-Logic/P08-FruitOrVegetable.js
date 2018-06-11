function solve(word) {
    var fruits = [ "banana", "apple", "kiwi", "cherry", "lemon", "grapes", "peach" ];
    var vegetables = [ "tomato", "cucumber", "pepper", "onion", "garlic", "parsley" ];
    if (fruits.includes(word)) {
        console.log("fruit");
    } else if (vegetables.includes(word)) {
        console.log("vegetable");
    } else {
        console.log("unknown");
    }
}