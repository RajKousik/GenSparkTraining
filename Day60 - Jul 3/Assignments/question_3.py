# Lists

if __name__ == '__main__':
    N = int(input())
    my_list = list()

    for _ in range(N):
        query = input().split()
        if query[0] == "insert":
            my_list.insert(int(query[1]), int(query[2]))
        elif query[0] == "print":
            print(my_list)
        elif query[0] == "remove":
            my_list.remove(int(query[1]))
        elif query[0] == "append":
            my_list.append(int(query[1]))
        elif query[0] == "sort":
            my_list = sorted(my_list)
        elif query[0] == "pop":
            my_list.pop()
        elif query[0] == "reverse":
            my_list.reverse()