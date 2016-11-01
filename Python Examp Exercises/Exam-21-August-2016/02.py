# Result - 77/100
# https://judge.softuni.bg/Contests/Practice/Index/273#1

try:
    bike_price = float(input())
    if bike_price <= 0:
        raise ValueError("Bike price must be positive")
    daily_saved_capital = float(input())
    capital_spent_on_tenth_day = float(input())
    if capital_spent_on_tenth_day < daily_saved_capital:
        raise ValueError("Weekly capital should be bigger than daily")
    if bike_price < daily_saved_capital:
        raise ValueError("You earn more money than your bike")
    if (daily_saved_capital * 10) - capital_spent_on_tenth_day > 0:
        day = 0
        capital = 0
        while True:
            if capital < bike_price:
                capital += daily_saved_capital
                day += 1
                if day % 10 == 0:
                    capital -= capital_spent_on_tenth_day
            else:
                break
        print(day)
    else:
        print("NO BIKE FOR YOU")
except:
    print("INVALID INPUT")