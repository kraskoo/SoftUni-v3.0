# Result - 100/100
# https://judge.softuni.bg/Contests/Practice/Index/273#3

import iso8601

try:
    file_name = input()
    percentage_up = float(input())
    if percentage_up < 0:
        raise ValueError("Percentage rise should be positive")
    records = {}
    with open(file_name, 'r', encoding='utf-8') as file:
        for line in file:
            data = line.split(',')
            date = iso8601.parse_date(data[0])
            price = float(data[1])
            if price < 0:
                raise ValueError("Price must be positive")
            if date not in records:
                records[date] = price
    records = sorted(records.items())
    previous_day_income = records[0][1]
    records_with_drastic_percentage_up = {}
    for rec in records[1:]:
        current_day_income = rec[1]
        percentage_difference = ((current_day_income - previous_day_income) / previous_day_income) * 100
        if percentage_difference >= percentage_up:
            date = rec[0]
            day = date.day
            if len(str(day)) == 1:
                day = "0{}".format(day)
            month = date.month
            if len(str(month)) == 1:
                month = "0{}".format(month)
            year = date.year
            date = "{}-{}-{}".format(year, month, day)
            if date not in records_with_drastic_percentage_up:
                records_with_drastic_percentage_up[date] = percentage_difference
        previous_day_income = current_day_income
    records_with_drastic_percentage_up = sorted(records_with_drastic_percentage_up.items())
    if len(records_with_drastic_percentage_up) > 0:
        for (key, value) in records_with_drastic_percentage_up:
            print("{} {:.2f}".format(key, value))
    else:
        print("NO DRASTIC CHANGES; RECORDS COUNT: {}".format(len(records)))
except:
    print("INVALID INPUT")