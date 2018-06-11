function solve(y, m, d) {
    var currentDate = new Date(y, m - 1, d);
    let oneDay = 24 * 60 * 60 * 1000;
    var nextDay = new Date(currentDate.valueOf() + oneDay);
    console.log(`${nextDay.getFullYear()}-${nextDay.getMonth() + 1}-${nextDay.getDate()}`);
}