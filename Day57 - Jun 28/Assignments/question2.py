def is_prime(num: int) -> bool:
    """
    Check if a number is prime.

    :param num: Number to check
    :return: True if the number is prime, False otherwise
    """
    if num <= 1:
        return False
    for i in range(2, int(num ** 0.5) + 1):
        if num % i == 0:
            return False
    return True

def prime_numbers_up_to(n: int) -> list:
    """
    Generate a list of prime numbers up to a given number.

    :param n: Upper limit
    :return: List of prime numbers up to n
    """
    try:
        if n < 2:
            return []
        primes = []
        for i in range(2, n + 1):
            if is_prime(i):
                primes.append(i)
        return primes
    except Exception as e:
        print(f"An error occurred: {e}")
        return []


upper_limit = 50
print(f"Prime numbers up to {upper_limit}: {prime_numbers_up_to(upper_limit)}")
