# Define a set
my_set = {1, 2, 3, 4, 5}

# Adding elements to a set
my_set.add(6)

# Removing elements from a set
my_set.remove(3)

# Checking membership
is_in_set = 4 in my_set  # True

# Set operations
another_set = {4, 5, 6, 7, 8}
union_set = my_set | another_set  # {1, 2, 4, 5, 6, 7, 8}
intersection_set = my_set & another_set  # {4, 5, 6}
difference_set = my_set - another_set  # {1, 2}
symmetric_difference_set = my_set ^ another_set  # {1, 2, 7, 8}

print(f"Original Set: {my_set}")
print(f"Union: {union_set}")
print(f"Intersection: {intersection_set}")
print(f"Difference: {difference_set}")
print(f"Symmetric Difference: {symmetric_difference_set}")
print(f"Is 4 in Set: {is_in_set}")
