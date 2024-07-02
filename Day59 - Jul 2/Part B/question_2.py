# collections.Counter()

# Enter your code here. Read input from STDIN. Print output to STDOUT
from collections import Counter

def calculate_profit(sales, stock):
    profit = 0
    
    for i in range(sales):
        sale = input().split(" ")
        shoe_size, price = sale[0], int(sale[1])
        
        if shoe_size in stock and stock[shoe_size] > 0:
            profit += price
            stock[shoe_size] -=1
            
    return profit

len = int(input())
stock = dict(Counter([i for i in input().split(" ")]))
sales = int(input())

print(calculate_profit(sales, stock))