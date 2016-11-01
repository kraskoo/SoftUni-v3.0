# https://judge.softuni.bg/Contests/Practice/Index/161#0

import math
FLACON_COVERAGE = 1.76

try:
    width = float(input())
    height = float(input())
    print(math.ceil((width * height) / FLACON_COVERAGE))
except:
    print("INVALID INPUT")