# https://judge.softuni.bg/Contests/Practice/Index/162#1

X_DIRECTION = ['left', 'right']
Y_DIRECTION = ['up', 'down']
VALID_DIRECTION = set(X_DIRECTION + Y_DIRECTION)

try:
    points_by_x = []
    points_by_y = []
    file_name = input()
    with open(file_name, encoding='utf-8') as file:
        each_line = [l.strip() for l in file if l[0].isalpha()]
        line = [(direct, float(value)) for direct, value in (each.split() for each in each_line)]
        if not any(l for l in line if l[0] is "left" or "right" or "up" or "down"):
            raise ValueError('Incorrect value')

        for direct, value in line:
            if direct == 'right':
                points_by_x.append(value)
            if direct == 'left':
                points_by_x.append(value * -1)
            if direct == 'up':
                points_by_y.append(value)
            if direct == 'down':
                points_by_y.append(value * -1)
        x = sum(points_by_x)
        y = sum(points_by_y)
        print("X {:.3f}".format(x))
        print("Y {:.3f}".format(y))
except Exception as e:
    print("INVALID INPUT")