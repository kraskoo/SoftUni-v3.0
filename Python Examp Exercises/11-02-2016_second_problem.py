# https://judge.softuni.bg/Contests/Practice/Index/161#1

ORD_A = ord('A')
ORD_Z = ord('Z')
ORD_DIFF = ORD_Z - ORD_A + 1

try:
    key = int(input())
    message = input()
    if not message:
        raise ValueError("The message's value must be non-empty")
    encrypted = []
    characters = [ord(c) for c in message]
    for c in characters:
        if ORD_A <= c <= ORD_Z:
            c += key
            if c > ORD_Z:
                c -= ORD_DIFF
        encrypted.append(chr(c))
    encrypted_message = ''.join(encrypted)
    print(encrypted_message)
except:
    print("INVALID INPUT")