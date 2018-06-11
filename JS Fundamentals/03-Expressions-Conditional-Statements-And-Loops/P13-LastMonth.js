function solve(arr) {
    [d, m, y] = arr;
    let currentDate = new Date(y, m - 1, d);
    const oneDay = 24 * 60 * 60 * 1000;
    let lastDayInPreviousMonth = new Date(currentDate.valueOf() - (d * oneDay));
    console.log(lastDayInPreviousMonth.getDate());
}