# https://judge.softuni.bg/Contests/Practice/Index/161#2

try:
    file_name = input()
    word = input()
    sorted_word = sorted(word)

    anagrams = []
    with open(file_name, encoding='utf-8') as file:
        for line in file:
            word_in_text = line.strip()
            if sorted(word_in_text) == sorted_word and word_in_text != word:
                anagrams.append(word_in_text)
        if anagrams:
            anagrams.sort()
            print('\n'.join(anagrams))
        else:
            print("NO ANAGRAMS")
except:
    print("INVALID INPUT")