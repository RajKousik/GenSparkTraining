def is_prime(num):
    
    if num <= 1:
        print(f"{num} is not a prime number.")
        return
    
    for i in range(2, int(num ** 0.5) + 1):
        if num % i == 0:
            print(f"{num} is not a prime number.")
            return
    
    print(f"{num} is a prime number.")

num = int(input("Enter a number: "))
is_prime(num)