# Finding the percentage

if __name__ == '__main__':
    n = int(input())
    student_marks = {}
    for _ in range(n):
        name, *line = input().split()
        scores = list(map(float, line))
        student_marks[name] = scores
    query_name = input()
    
    marks = student_marks[query_name]
    sum_of_marks = 0
    for mark in marks:
        sum_of_marks += mark
    avg_mark = sum_of_marks/len(marks)
    print("{:.2f}".format(avg_mark))