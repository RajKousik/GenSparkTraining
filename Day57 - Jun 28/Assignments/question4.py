import random

def generate_secret_number() -> str:
    """
    Generate a 4-digit secret number with unique digits.

    :return: 4-digit secret number as a string
    """
    digits = list(range(10))
    random.shuffle(digits)
    secret_number = ''.join(map(str, digits[:4]))
    return secret_number

def get_cows_and_bulls(secret: str, guess: str) -> tuple:
    """
    Calculate the number of cows and bulls for a given guess.

    :param secret: The secret number
    :param guess: The player's guess
    :return: Tuple containing the number of cows and bulls
    """
    cows = bulls = 0
    for i in range(4):
        if guess[i] == secret[i]:
            bulls += 1
        elif guess[i] in secret:
            cows += 1
    return cows, bulls

def play_cow_and_bull():
    """
    Main function to play the Cow and Bull game.
    """
    try:
        secret_number = generate_secret_number()
        attempts = 0
        while True:
            guess = input("Enter your guess (4 unique digits): ")
            if len(guess) != 4 or not guess.isdigit() or len(set(guess)) != 4:
                print("Invalid guess. Please enter 4 unique digits.")
                continue
            attempts += 1
            cows, bulls = get_cows_and_bulls(secret_number, guess)
            print(f"Cows: {cows}, Bulls: {bulls}")
            if bulls == 4:
                print(f"Congratulations! You've guessed the secret number {secret_number} in {attempts} attempts.")
                break
    except Exception as e:
        print(f"An error occurred: {e}")


play_cow_and_bull()
