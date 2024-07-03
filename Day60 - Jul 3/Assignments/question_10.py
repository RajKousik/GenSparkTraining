# String Validators


if __name__ == '__main__':
    s = input()
    
    is_alnum, is_alpha, is_digit, is_lower, is_upper = False, False, False, False, False
    
    for ch in s:
        if ch.isalnum():
            is_alnum = True
        if ch.isalpha():
            is_alpha = True
        if ch.isdigit():
            is_digit = True
        if ch.islower():
            is_lower = True
        if ch.isupper():
            is_upper = True
            
    print(is_alnum, is_alpha, is_digit, is_lower, is_upper, sep='\n')