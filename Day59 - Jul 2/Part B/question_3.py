# Company Logo

import math
import os
import random
import re
import sys



if __name__ == '__main__':
    s = input()
    
    letters = {l:s.count(l) for l in s}

    top_three = sorted(sorted(letters), key=lambda x: letters[x], reverse=True)[:3]
    print('\n'.join([f'{l} {letters[l]}' for l in top_three]))

