# Multiply Strings

class Solution:
    def multiply(self, num1: str, num2: str) -> str:
        N = len(num1) + len(num2)
        res=[0]*N
        
        for i in range(len(num1)-1, -1, -1):
            for j in range(len(num2)-1, -1, -1):
                value = int(num1[i]) * int(num2[j])
                pos1, pos2 = i+j, i+j+1
                
                sum = value + res[pos2]
                
                res[pos2] = sum % 10
                res[pos1] += sum // 10
                
        i = 0
        
        while i < len(res) and res[i] == 0:
            i+=1
            
        result = ''.join(str(ch) for ch in res[i:])
        return result if result else '0'
        