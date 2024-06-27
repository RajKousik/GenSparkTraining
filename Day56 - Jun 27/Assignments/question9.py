# from itertools import permutations

# def permutations_of_string():
#     string = input("Enter a string: ")
#     perms = permutations(string)
#     for perm in perms:
#         print(''.join(perm))

# permutations_of_string()


def permutations_of_string():
    string = input("Enter a string: ")
    permute(list(string), 0, len(string) - 1)

def permute(s, l, r):
    if l == r:
        print(''.join(s))
    else:
        for i in range(l, r + 1):
            s[l], s[i] = s[i], s[l]  # swap
            permute(s, l + 1, r)
            s[l], s[i] = s[i], s[l]  # backtrack

permutations_of_string()
