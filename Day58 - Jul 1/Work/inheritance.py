class Person:
    """
    A base class to represent a person.
    """

    def __init__(self, name: str, age: int):
        """
        Initialize the person's name and age.

        :param name: Name of the person
        :param age: Age of the person
        """
        self.name = name
        self.age = age

    def introduce(self):
        """
        Introduce the person.
        """
        print(f"Hello, my name is {self.name} and I am {self.age} years old.")


class Student(Person):
    """
    A class to represent a student, inheriting from Person.
    """

    def __init__(self, name: str, age: int, student_id: str, courses: list):
        """
        Initialize the student with name, age, student ID, and enrolled courses.

        :param name: Name of the student
        :param age: Age of the student
        :param student_id: Student ID
        :param courses: List of courses the student is enrolled in
        """
        super().__init__(name, age)
        self.student_id = student_id
        self.courses = courses

    def introduce(self):
        """
        Introduce the student.
        """
        super().introduce()
        print(f"I am a student with ID: {self.student_id}. I am enrolled in the following courses: {', '.join(self.courses)}")

    def add_course(self, course: str):
        """
        Add a course to the student's course list.

        :param course: Course to add
        """
        if course not in self.courses:
            self.courses.append(course)
            print(f"Course {course} has been added to {self.name}'s course list.")
        else:
            print(f"Course {course} is already in the course list.")


class Teacher(Person):
    """
    A class to represent a teacher, inheriting from Person.
    """

    def __init__(self, name: str, age: int, employee_id: str, subjects: list):
        """
        Initialize the teacher with name, age, employee ID, and subjects taught.

        :param name: Name of the teacher
        :param age: Age of the teacher
        :param employee_id: Employee ID
        :param subjects: List of subjects the teacher teaches
        """
        super().__init__(name, age)
        self.employee_id = employee_id
        self.subjects = subjects

    def introduce(self):
        """
        Introduce the teacher.
        """
        super().introduce()
        print(f"I am a teacher with Employee ID: {self.employee_id}. I teach the following subjects: {', '.join(self.subjects)}")

    def add_subject(self, subject: str):
        """
        Add a subject to the teacher's subject list.

        :param subject: Subject to add
        """
        if subject not in self.subjects:
            self.subjects.append(subject)
            print(f"Subject {subject} has been added to {self.name}'s subjects.")
        else:
            print(f"Subject {subject} is already in the subjects list.")



    # Create a student
student1 = Student("Alice", 20, "S12345", ["Math", "Science"])
student1.introduce()
student1.add_course("History")
student1.introduce()

print("\n")

# Create a teacher
teacher1 = Teacher("Mr. Smith", 40, "T98765", ["Math", "Physics"])
teacher1.introduce()
teacher1.add_subject("Chemistry")
teacher1.introduce()
