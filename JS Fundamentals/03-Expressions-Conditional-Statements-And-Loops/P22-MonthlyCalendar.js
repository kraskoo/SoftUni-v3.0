function solve(input) {
    [day, month, year] = input;
    month--;
    let months = [31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31];
    if ((year % 4 == 0 && year % 100 != 0) || year % 400 == 0) {
        months[1] = 29;
    }

    let str = "<table>\n";
    str += '  <tr><th>Sun</th><th>Mon</th><th>Tue</th><th>Wed</th><th>Thu</th><th>Fri</th><th>Sat</th></tr>\n';
    let date = new Date(year, month, 1);
    let dayOfWeek = date.getDay();
    let firstDayPrevMonth = months[(month - 1 + 12) % 12] - dayOfWeek;
    let week = 0;
    if (dayOfWeek > 0) {
        str += "  <tr>";
    }

    for (let i = 1; i <= dayOfWeek; i++) {
        str += `<td class="prev-month">${(firstDayPrevMonth + i)}</td>`;
        week++;
    }

    let monthDays = months[month];
    for (let i = 1; i <= monthDays; i++) {
        if (week == 0) {
            str += '  <tr>';
        }

        if (day == i) {
            str += '<td class="today">' + i + '</td>';
        } else {
            str += '<td>' + i + '</td>';
        }

        week++;
        if (week == 7) {
            str += '</tr>\n';
            week=0;
        }
    }

    for (let i = 1; week != 0; i++) {
        str += `<td class="next-month">${i}</td>`;
        week++;
        if (week == 7) {
            str += '</tr>\n';
            week = 0;
        }
    }

    str += "</table>";
    console.log(str);
}