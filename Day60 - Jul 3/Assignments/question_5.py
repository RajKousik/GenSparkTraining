# sWAP cASE

def swap_case(s):
    str_swapped = ""
    
    for ch in s:
        if ch.isupper():
            str_swapped += ch.lower()
        elif ch.islower():
            str_swapped += ch.upper()
        else:
            str_swapped += ch
    return str_swapped

if __name__ == '__main__':
    s = input()
    result = swap_case(s)
    print(result)