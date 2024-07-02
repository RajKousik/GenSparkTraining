# Generate Parentheses

class Solution:
    
    def generateParenthesis(self, n: int) -> List[str]: # type: ignore
        def dfs(left, right, s):
            if len(s) == n * 2:
                res.append(s)

            if left < n:
                dfs(left+1, right, s + "(")
            if right < left:
                dfs(left, right+1, s + ")")
        res = []
        dfs(0, 0, '')
        return res