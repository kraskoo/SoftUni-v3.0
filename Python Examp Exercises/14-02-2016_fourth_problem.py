# https://judge.softuni.bg/Contests/Practice/Index/162#3

import iso8601

try:
    file_name = input()
    input_data = []
    cities = set()
    dates = {}
    with open(file_name, encoding='utf-8') as file:
        input_data = [l.strip().split(',') for l in file if l.strip()]
    if not any(input_data):
        raise ValueError("The data must have at least one parameter")
    for date, city, temp in input_data:
        cities.add(city)
        if date not in dates:
            dates[date] = set()
        dates[date].add(city)
    missing_data = {}
    for key, value in dates.items():
        difference = cities.symmetric_difference(value)
        if any(difference):
            dated = key.split('-')
            parsed_key = iso8601.parse_date(key)
            missing_data[parsed_key] = difference
    if any(missing_data):
        missing_data = sorted(missing_data.items())
        for key, value in missing_data:
            month = str(key.month)
            day = str(key.day)
            sorted_value = list(value)
            sorted_value.sort()
            if len(month) is 1:
                month = "0{}".format(month)
            if len(day) is 1:
                day = "0{}".format(day)
            date_to_str = "{}-{}-{}".format(key.year, month, day)
            print("{},{}".format(date_to_str, ','.join(sorted_value)))
    else:
        print("ALL DATA AVAILABLE")
except:
    print("INVALID INPUT")