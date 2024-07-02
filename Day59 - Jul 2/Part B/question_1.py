# Merge the Tools!

def merge_the_tools(string, k):
    # your code goes here
    result = []
    for i in range(0, len(string), k):
        sub_result = ""
        for ch in string[i:i+k]:
            if ch not in sub_result:
                sub_result = sub_result + ch
        result.append(sub_result)
    print("\n".join(result)) 

if __name__ == '__main__':
    string, k = input(), int(input())
    merge_the_tools(string, k)