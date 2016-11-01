# https://judge.softuni.bg/Contests/Practice/Index/161#3

try:
    file_name = input()
    fridge_log = []
    with open(file_name, encoding='utf-8') as file:
        for line in file:
            csv_data = line.strip()
            data = [d for d in list(csv_data.split(','))]
            fridge_log.append((data[0], float(data[1])))
    previous_open = None
    for curr_ts, curr_temp in fridge_log:
        next_open = curr_temp
        if previous_open is not None:
            if next_open - previous_open >= 4:
                print(curr_ts)
        previous_open = next_open

except Exception:
    print("INVALID INPUT")