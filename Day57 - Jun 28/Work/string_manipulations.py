# Define a sample string
sample_string = "Hello, World!"

# String length
length = len(sample_string)  # 13

# String concatenation
concat_string = sample_string + " How are you?"  # "Hello, World! How are you?"

# String repetition
repeated_string = sample_string * 2  # "Hello, World!Hello, World!"

# String slicing
substring = sample_string[7:12]  # "World"

# String methods
upper_string = sample_string.upper()  # "HELLO, WORLD!"
lower_string = sample_string.lower()  # "hello, world!"
title_string = sample_string.title()  # "Hello, World!"
swapcase_string = sample_string.swapcase()  # "hELLO, wORLD!"

# Finding a substring
index = sample_string.find("World")  # 7

# Replacing a substring
replaced_string = sample_string.replace("World", "Universe")  # "Hello, Universe!"

# Splitting a string
split_string = sample_string.split(", ")  # ["Hello", "World!"]

# Joining strings
joined_string = ", ".join(["Hello", "World"])  # "Hello, World"

# Stripping whitespace
whitespace_string = "   Hello, World!   "
stripped_string = whitespace_string.strip()  # "Hello, World!"

print(f"Original String: {sample_string}")
print(f"Length: {length}")
print(f"Concatenated String: {concat_string}")
print(f"Repeated String: {repeated_string}")
print(f"Sliced String: {substring}")
print(f"Uppercase: {upper_string}")
print(f"Lowercase: {lower_string}")
print(f"Title Case: {title_string}")
print(f"Swap Case: {swapcase_string}")
print(f"Index of 'World': {index}")
print(f"Replaced String: {replaced_string}")
print(f"Split String: {split_string}")
print(f"Joined String: {joined_string}")
print(f"Stripped String: {stripped_string}")
