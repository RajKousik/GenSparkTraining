# A simple function with no parameters and no return value
def greet():
    print("Hello, World!")

greet()

# A function with parameters
def add(a, b):
    return a + b

result = add(5, 3)  # 8
print(f"Addition Result: {result}")

# A function with default parameters
def greet_person(name="Guest"):
    print(f"Hello, {name}!")

greet_person("Alice")
greet_person()  # Uses default value

# A function with variable number of arguments
def sum_all(*args):
    return sum(args)

total = sum_all(1, 2, 3, 4, 5)  # 15
print(f"Sum of All: {total}")

# A function with keyword arguments
def print_info(**kwargs):
    for key, value in kwargs.items():
        print(f"{key}: {value}")

print_info(name="Alice", age=30, city="New York")
