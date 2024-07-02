# Jump Game

class Solution:
    def canJump(self, nums: List[int]) -> bool: #type: ignore
        n = len(nums)
        
        maxReach = 0
        for i in range(len(nums)):
            if(i <= maxReach):
                maxReach = max(maxReach, i + nums[i]);
                if maxReach >= n-1:
                    return 1
        return 0