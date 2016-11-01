# Result - 80/100
# https://judge.softuni.bg/Contests/Practice/Index/273#0

try:
    input_sequence = input()
    array_numbers = []
    for n in input_sequence.split():
        if n != ' ':
            array_numbers.append(int(n))

    array_numbers.reverse()
    previous_number = array_numbers[0]
    break_sequence_number = -1
    index = len(array_numbers) - 1
    for num in array_numbers[1:]:
        if num <= previous_number:
            previous_number = num
        else:
            break_sequence_number = index
            break
        index -= 1
    if break_sequence_number != -1:
        print(break_sequence_number)
    else:
        print("SORTED")
except:
    print("INVALID INPUT")