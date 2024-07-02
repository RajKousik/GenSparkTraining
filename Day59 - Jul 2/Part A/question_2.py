#!/bin/python3

import math
import os
import random
import re
import sys


#
# Complete the 'transformSentence' function below.
#
# The function is expected to return a STRING.
# The function accepts STRING sentence as parameter.
#

def transformSentence(sentence):
    # Write your code here
    res = ''
    index = 0
    
    for letter in sentence:
        if index == 0:
            res += letter
        elif letter == ' ':
            res += letter
        else:
            prevLetter = sentence[index - 1]
            if prevLetter == ' ':
                res += letter
            elif prevLetter.lower() < letter.lower():
                res += letter.upper()
            elif prevLetter.lower() > letter.lower():
                res += letter.lower()
            else:
                res += letter
        index += 1
    return res
    

if __name__ == '__main__':

    sentence = input()

    result = transformSentence(sentence)

    print(f"Result is {result}")

