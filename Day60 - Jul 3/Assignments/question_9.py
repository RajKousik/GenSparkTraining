# Find a string

def count_substring(string, sub_string):
    
    substring_count = 0
    
    for index, ch in enumerate(string):
        if index+len(sub_string) > len(string):
            break
        if string[index:index+len(sub_string)] == sub_string:
            substring_count += 1
    
    return substring_count

if __name__ == '__main__':
    string = input().strip()
    sub_string = input().strip()
    
    count = count_substring(string, sub_string)
    print(count)