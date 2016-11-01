# Result - 85 / 100
# https://judge.softuni.bg/Contests/Practice/Index/273#2

PARENTHESIS = {'(', ')'}

try:
    input_line = input()
    open_parenthesis = 0
    closed_parenthesis = 0
    line_to_char_array = [ch for ch in input_line]
    if not any(set(line_to_char_array).intersection(PARENTHESIS)):
        raise ValueError("Input line must have at least one parenthesis")
    for ch in line_to_char_array:
        if ch is '(':
            open_parenthesis += 1
            if closed_parenthesis > open_parenthesis:
                break
        if ch is ')':
            closed_parenthesis += 1
            if closed_parenthesis > open_parenthesis:
                break

    if open_parenthesis == closed_parenthesis:
        result = open_parenthesis
        print("OK {}".format(result))
    else:
        input_length = len(input_line)
        print("WRONG {}".format(input_length))
except:
    print("INVALID INPUT")
