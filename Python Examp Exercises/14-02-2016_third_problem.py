# https://judge.softuni.bg/Contests/Practice/Index/162#2

import math

def validate_dimension(val):
    if val <= 0:
        raise ValueError("value must be positive")

try:
    litres = float(input())
    validate_dimension(litres)
    litres *= 1000
    file_name = input()
    lines = None
    with open(file_name, encoding='utf-8') as file:
        lines = [(data[0], float(data[1]), float(data[2])) for data in
                 (l.strip().split(',') for l in file if l.strip())]

    first_value = lines[0]
    min_cylinder = first_value[0]
    min_radius = first_value[1]
    validate_dimension(min_radius)
    min_height = first_value[2]
    validate_dimension(min_height)
    min_cylinder_capacity = math.pi * math.pow(min_radius, 2) * min_height
    min_capacity = -1
    min_containable_cylinder = "NO SUITABLE CONTAINER"
    if min_cylinder_capacity >= litres:
        min_capacity = min_cylinder_capacity
        min_containable_cylinder = min_cylinder

    for line in lines[1:]:
        name = line[0]
        radius = line[1]
        validate_dimension(radius)
        height = line[2]
        validate_dimension(height)
        cylinder_capacity = math.pi * math.pow(radius, 2) * height
        if (litres <= cylinder_capacity < min_capacity) or (min_capacity == -1 and cylinder_capacity >= litres):
            min_capacity = cylinder_capacity
            min_containable_cylinder = name
    print(min_containable_cylinder)

except:
    print("INVALID INPUT")