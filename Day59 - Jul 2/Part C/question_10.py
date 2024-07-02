# Text Justification

class Solution:
    def fullJustify(self, words: List[str], maxWidth: int) -> List[str]: #type: ignore
            
        result, current_list, num_of_letters = [],[], 0

        
        for word in words:
            if num_of_letters + len(word) + len(current_list) > maxWidth:
                
                size = max(1, len(current_list)-1)
                
                for i in range(maxWidth-num_of_letters):
                    index = i%size
                    current_list[index] += ' ' 
                
                result.append("".join(current_list))
                current_list, num_of_letters = [], 0
            

            current_list.append(word)
            num_of_letters += len(word)
        
        # form last line by join with space and left justify to maxWidth using ljust (python method)
        # that means pad additional spaces to the right to make string length equal to maxWidth
        result.append(' '.join(current_list).ljust(maxWidth))
        
        return result