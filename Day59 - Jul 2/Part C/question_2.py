# Zigzag Conversion

class Solution:
    def convert(self, s: str, numRows: int) -> str:
        if numRows == 1 or numRows >= len(s):
            return s

        idx, d = 0, 1
        rows = [''] * numRows

        for char in s:
            rows[idx] += char
            if idx == 0:
                d = 1
            elif idx == numRows - 1:
                d = -1
            idx += d

        return ''.join(rows) 