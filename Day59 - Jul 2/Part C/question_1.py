# Longest Substring Without Repeating Characters

class Solution:
    def lengthOfLongestSubstring(self, s: str) -> int:
        left = 0
        length = 0
        seen = {}
        
        for right in range(len(s)):
            
            char = s[right]
            
            if char in seen and seen[char] >= left: #to understand use testcase 'abba'
                left = seen[char] + 1
            else:
                length = max(length, right - left + 1)
            
            seen[char] = right
            
        return length
            