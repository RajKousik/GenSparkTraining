def print_pyramid():
    rows = int(input("Enter the number of rows: "))
    # for i in range(1, rows + 1):
    #     print(' ' * (rows - i) + '*' * (2 * i - 1))

    for i in range(1, rows + 1):
        for s in range(rows-i):
            print(' ', end='')
        for j in range(0, 2 * i - 1):
            print('*', end='')
        print()


print_pyramid()
