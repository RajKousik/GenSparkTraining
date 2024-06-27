# This is a Python script to print "Hello, World!"
print("Understanding Execution")
print("Hello, World!")


print()
print("Input")
# Taking input from the user
name = input("Enter your name: ")
print(f"Hello, {name}!")



print()
print("Output")
# Displaying output to the user
print("This is an output example.")



# Different datatypes in Python
integer_num = 10
float_num = 10.5
string_text = "Hello"
boolean_value = True

print()
print("Datatypes")
print(type(integer_num))  # Output: <class 'int'>
print(type(float_num))    # Output: <class 'float'>
print(type(string_text))  # Output: <class 'str'>
print(type(boolean_value))  # Output: <class 'bool'>


# Arithmetic operators
a = 10
b = 3

print()
print("Operators")
print(a + b)  # Addition
print(a - b)  # Subtraction
print(a * b)  # Multiplication
print(a / b)  # Division
print(a // b)  # Division
print(a % b)  # Modulus

# Comparison operators
print()
print("Comparison operators")
print(a == b)  # Equals
print(a != b)  # Not equals
print(a > b)   # Greater than
print(a < b)   # Less than

# Logical operators
print()
print("Logical operators")
print(a > 5 and b < 5)  # Logical AND
print(a > 5 or b > 5)   # Logical OR
print(not(a > 5))       # Logical NOT


# Conditional statements example
print()
print("Conditional statements")
num = 10

if num > 0:
    print("Positive number")
elif num == 0:
    print("Zero")
else:
    print("Negative number")



# Using a for loop
print()
print("Iterations")
print("For Loop:")
for i in range(5):
    print(f"Iteration {i}")

print()
print("While Loop:")
# Using a while loop
count = 0
while count < 5:
    print(f"Count is {count}")
    count += 1



# Defining and using a method (function)
print()
print("Methods")
def greet(name):
    return f"Hello, {name}!"

print(greet("Alice"))



# Function with parameters
print()
print("Method with Parameters")
def add(a, b):
    return a + b

result = add(3, 4)
print(result)  # Output: 7


# Function with return statement
print()
print("Method with return value")
def multiply(a, b):
    return a * b

product = multiply(4, 5)
print(product)  # Output: 20



# List and indexing example
print()
print("List and Indexing")
fruits = ["apple", "banana", "cherry"]

print(fruits[0])  # Accessing the first item
print(fruits[1])  # Accessing the second item
print(fruits[-1])  # Accessing the last item

# Slicing a list
print(fruits[1:3])  # Output: ['banana', 'cherry']
