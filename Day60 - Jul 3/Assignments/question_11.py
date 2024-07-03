# Text Alignment

# Enter your code here. Read input from STDIN. Print output to STDOUT

thickness = int(input())
ch = 'H'


for i in range(thickness):
    print((ch*i).rjust(thickness-1) + ch + (ch*i).ljust(thickness-1))
    
for i in range(thickness+1):
    print((ch*thickness).center(thickness*2) + (ch*thickness).center(thickness*6))
    
for i in range((thickness+1)//2):
    print((ch*thickness*5).center(thickness*6))
    
for i in range(thickness+1):
    print((ch*thickness).center(thickness*2) + (ch*thickness).center(thickness*6))
    
for i in range(thickness):
    print(((ch * (thickness - i - 1)).rjust(thickness) + ch + (ch * (thickness - i - 1)).ljust(thickness)).rjust(thickness * 6))
    