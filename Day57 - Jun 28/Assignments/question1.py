def longest_substring_without_repeating_characters(s: str) -> str:
    """
    Find the longest substring without repeating characters in the given string.

    :param s: Input string
    :return: Longest substring without repeating characters
    """
    try:
        n = len(s)
        if n == 0:
            return ""

        char_index_map = {}
        max_length = 0
        start = 0
        longest_substr = ""

        for end in range(n):
            if s[end] in char_index_map:
                start = max(start, char_index_map[s[end]] + 1)
            char_index_map[s[end]] = end
            if end - start + 1 > max_length:
                max_length = end - start + 1
                longest_substr = s[start:end + 1]

        return longest_substr
    except Exception as e:
        print(f"An error occurred: {e}")
        return ""

# Example usage

input_string = "abcabcdbb"
print(f"Longest substring without repeating characters: {longest_substring_without_repeating_characters(input_string)}")
