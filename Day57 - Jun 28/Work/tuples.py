# Define a tuple
my_tuple = (1, 2, 3, 4, 5)

# Accessing elements
first_element = my_tuple[0]  # 1
last_element = my_tuple[-1]  # 5

# Slicing a tuple
sub_tuple = my_tuple[1:4]  # (2, 3, 4)

# Tuple unpacking
a, b, c, d, e = my_tuple

# Nesting tuples
nested_tuple = ((1, 2), (3, 4))

print(f"Original Tuple: {my_tuple}")
print(f"First Element: {first_element}")
print(f"Last Element: {last_element}")
print(f"Sliced Tuple: {sub_tuple}")
print(f"Tuple Unpacking: {a}, {b}, {c}, {d}, {e}")
print(f"Nested Tuple: {nested_tuple}")


# Throws Error
# 'tuple' object does not support item assignment
nested_tuple[0] = (1, 2)
