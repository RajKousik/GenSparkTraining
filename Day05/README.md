# DAY 5

## Topics Covered

* Selection Statements

* Control Statements

* Employee Tracker CRUD

* Logical Problems


## Logical Problems


#### [Bulls and Cows](https://leetcode.com/problems/bulls-and-cows/description/)

```
public class Solution {
    public string GetHint(string secret, string guess) {
        List<char> sChars = new List<char>();
        List<char> gChars = new List<char>();
        
        int bullCount = 0;
        int cowCount = 0;
        
        for (int i =0; i<guess.Length; i++)
        {
            if (secret[i] == guess[i])
            {
                bullCount++;
            }
            else
            {
                sChars.Add(secret[i]);
                gChars.Add(guess[i]);
            }                
        }
        
        for(int i=0; i<sChars.Count; i++)
        {
            if (gChars.Contains(sChars[i]))
            {
                cowCount++;
                gChars.Remove(sChars[i]);
            }
        }      
        
        return string.Format("{0}A{1}B", bullCount, cowCount);
    }
}
```
