# https://judge.softuni.bg/Contests/Practice/Index/161#4.

import re

IGNORE_SUFFIX = "/ws/"
URL_SEPARATOR = "?"
pattern = re.compile("url=\"(.+?)\".+?resp_t=\"(.+?)\"")

try:
    file_name = input()
    correct_founded = list()
    times_url = {}
    count_url = {}
    with open(file_name, encoding='utf-8') as file:
        for line in file:
            log = line.strip()
            searched_by_pattern = pattern.search(log)
            if searched_by_pattern:
                url_text = searched_by_pattern.group(1)
                separator_pos = url_text.find(URL_SEPARATOR)
                url = url_text
                if separator_pos is not -1:
                    url = url[:separator_pos]
                response_time = float(searched_by_pattern.group(2))
                if not url.endswith(IGNORE_SUFFIX):
                    if url not in times_url:
                        times_url[url] = 0
                        count_url[url] = 0
                    times_url[url] += response_time
                    count_url[url] += 1

    if times_url:
        avg_times = []
        for url in times_url:
            avg_time = times_url[url] / count_url[url]
            avg_times.append((avg_time, url))
        max_time = max(avg_times)
        print(max_time[1])
        print("{:.3f}".format(max_time[0]))
    else:
        print("NO DATA")
except:
    print("INVALID INPUT")