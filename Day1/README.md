Tasks
Create a new repository and try conflicting and resolve it.

Work on some logic problems:

- [Two Sum](https://leetcode.com/problems/two-sum/description/)

```
class Solution {
public:
    vector<int> twoSum(vector<int>& nums, int target) {
        
        unordered_map<int, int> umap;
        
        for(int i=0; i<nums.size(); i++)
        {
            int x = target - nums[i];
            if(umap.find(x) != umap.end())
            {
                return {umap[x], i};
            }
            umap[nums[i]] = i;
        }
        
        return {};
        
    }
};

```

![image](https://github.com/RajKousik/GenSparkTraining/assets/91744323/6b09cf8e-17e5-4bc1-8dd2-0a236dccb231)


- [Palindrome Number](https://leetcode.com/problems/palindrome-number/description/)

```

class Solution {
public:
    bool isPalindrome(int x) {
        if(x<0)
            return 0;
        int temp = x;
        long rev=0;
        while(x>0){
            int dig = x%10;
            rev = (rev*10) + dig;
            x=x/10;
        }
        return temp == rev;
    }
};

```
![image](https://github.com/RajKousik/GenSparkTraining/assets/91744323/55eb1e8c-efa9-4e32-a5ad-7a0de7c90aa2)

