# Day 57

## Work

### String Manipulations

- Demonstrates various string manipulation operations in Python.
- Includes examples of string length, concatenation, repetition, slicing, methods like `upper()`, `lower()`, `title()`, `swapcase()`, finding a substring, replacing a substring, splitting a string, joining strings, and stripping whitespace.

### Functions

- Demonstrates various function concepts in Python.
- Includes examples of simple functions, functions with parameters, default parameters, variable number of arguments, and keyword arguments.

### Set

- Demonstrates the usage of sets in Python.
- Includes examples of defining a set, adding elements, removing elements, checking membership, set operations like union, intersection, difference, and symmetric difference.

### Tuples

- Demonstrates the usage of tuples in Python.
- Includes examples of defining a tuple, accessing elements, slicing a tuple, tuple unpacking, nesting tuples, and iterating through a tuple.

### Dictionary

- Demonstrates the usage of dictionaries in Python.
- Includes examples of defining a dictionary, accessing values, adding a new key-value pair, updating a value, removing a key-value pair, iterating through a dictionary, and using dictionary methods like `keys()`, `values()`, and `items()`.

---

## Assignments

### 1. Longest Substring Without Repeating Characters. That is in a given string find the longest substring that does not contain any character twice.

- Find the longest substring without repeating characters in a given string.
- Uses a sliding window approach and a dictionary to keep track of character indices.

### 2. Print the list of prime numbers up to a given number

- Check if a number is prime.
- Uses a helper function `is_prime()` to check if a number is divisible by any number up to its square root.
- Generates a list of prime numbers up to a given number using a for loop and the `is_prime()` function.

### 3. Sort sore and name of players print the top 10

- Sort players by their scores in descending order.
- Takes a dictionary of player names and their scores as input.
- Uses the `sorted()` function with a lambda function as the key to sort the players based on their scores.
- Returns a list of tuples with player names and scores, sorted by scores.

### 4. Application to play the Cow and Bull game maintain score as well.

- Play the Cow and Bull game.
- Generates a 4-digit secret number with unique digits using the `generate_secret_number()` function.
- Prompts the user to enter their guess and calculates the number of cows and bulls using the `get_cows_and_bulls()` function.
- Continues until the user guesses the secret number correctly.

### 5. Credit card validation - Luhn check algorithm

- Validate a credit card number using the Luhn algorithm.
- Takes a credit card number as input and calculates the total sum using the Luhn algorithm.
- Returns True if the card number is valid, False otherwise.
