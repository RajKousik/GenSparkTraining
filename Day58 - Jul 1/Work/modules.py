# main_module.py
import math_operation

def main():
    """
    Main function to demonstrate module usage.
    """
    result = math_operation.add(5, 3)
    print(f"Addition result: {result}")

    result = math_operation.subtract(10, 4)
    print(f"Subtraction result: {result}")

if __name__ == "__main__":
    main()
