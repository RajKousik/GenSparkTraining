# Nested Lists

if __name__ == '__main__':
    students = []
    for _ in range(int(input())):
        name = input()
        score = float(input())
        students.append([name, score])
    mn = min(students, key=lambda x: x[1])
    non_lowest_student = sorted([student for student in students if student[1] > mn[1]], key= lambda x: x[1])
    second_grade_students = sorted([student for student in non_lowest_student if student[1] == non_lowest_student[0][1]])
    for student in second_grade_students:
        print(student[0])