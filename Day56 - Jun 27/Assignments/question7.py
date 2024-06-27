def average_of_primes():
    numbers = []
    for i in range(10):
        num = int(input(f"Enter number {i+1}: "))
        numbers.append(num)
    
    primes = [num for num in numbers if is_prime_number(num)]
    if primes:
        average = sum(primes) / len(primes)
        print(f"The average of the prime numbers is {average}")
    else:
        print("There are no prime numbers in the collection.")

def is_prime_number(num):
    if num <= 1:
        return False
    for i in range(2, int(num ** 0.5) + 1):
        if num % i == 0:
            return False
    return True

average_of_primes()