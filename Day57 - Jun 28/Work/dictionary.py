# Define a dictionary
my_dict = {"name": "Alice", "age": 30, "city": "New York"}

# Accessing values
name = my_dict["name"]  # "Alice"

# Adding a new key-value pair
my_dict["email"] = "alice@example.com"

# Updating a value
my_dict["age"] = 31

# Removing a key-value pair
del my_dict["city"]

# Iterating through a dictionary
for key, value in my_dict.items():
    print(f"{key}: {value}")

# Dictionary methods
keys = my_dict.keys()  # dict_keys(['name', 'age', 'email'])
values = my_dict.values()  # dict_values(['Alice', 31, 'alice@example.com'])
items = my_dict.items()  # dict_items([('name', 'Alice'), ('age', 31), ('email', 'alice@example.com')])

print(f"Dictionary: {my_dict}")
print(f"Keys: {keys}")
print(f"Values: {values}")
print(f"Items: {items}")
