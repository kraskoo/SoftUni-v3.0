function solve(minAge, fpName, fpAge, spName, spAge) {
    var arr = [];
    arr.push({ 'name': fpName, 'age': fpAge });
    arr.push({ 'name': spName, 'age': spAge });
    for (p in arr) {
        if (arr[p].age >= minAge) {
            console.log(arr[p]);
        }
    }
}