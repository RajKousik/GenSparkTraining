def write_to_file(filename, content):
    """
    Write content to a file.

    :param filename: Name of the file
    :param content: Content to write
    """
    try:
        with open(filename, 'w') as file:
            file.write(content)
            print(f"Content written to {filename}")
    except Exception as e:
        print(f"An error occurred while writing to the file: {e}")

def read_from_file(filename):
    """
    Read content from a file.

    :param filename: Name of the file
    :return: Content of the file
    """
    try:
        with open(filename, 'r') as file:
            content = file.read()
            return content
    except FileNotFoundError:
        print(f"Error: The file {filename} does not exist.")
        return None
    except Exception as e:
        print(f"An error occurred while reading from the file: {e}")
        return None


filename = "example.txt"
content_to_write = "Hello, this is a test file."

write_to_file(filename, content_to_write)
content = read_from_file(filename)
if content:
    print(f"Content of the file: {content}")
