def greet_with_salutation():
    name = input("Enter your name: ")
    gender = input("Enter your gender (M/F): ").strip().upper()
    salutation = "Mr." if gender == "M" else "Ms." if gender == "F" else ""
    print(f"Hello, {salutation} {name}!")

greet_with_salutation()