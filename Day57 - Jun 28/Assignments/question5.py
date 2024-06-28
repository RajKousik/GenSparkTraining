def luhn_check(card_number: str) -> bool:
    """
    Validate a credit card number using the Luhn algorithm.

    :param card_number: Credit card number as a string
    :return: True if the card number is valid, False otherwise
    """
    try:
        total_sum = 0
        reverse_digits = card_number[::-1]
        for i, digit in enumerate(reverse_digits):
            n = int(digit)
            if i % 2 == 1:
                n *= 2
                if n > 9:
                    n -= 9
            total_sum += n
        return total_sum % 10 == 0
    except Exception as e:
        print(f"An error occurred: {e}")
        return False


card_number = "4532015112830366"  # Example card number
if luhn_check(card_number):
    print(f"Card number {card_number} is valid.")
else:
    print(f"Card number {card_number} is invalid.")
