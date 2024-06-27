import re
from datetime import datetime

def validate_and_print_details():
    while True:
        name = input("Enter your name: ")
        if not name.isalpha():
            print("Invalid name. Please enter a valid name.")
            continue
        
        age = input("Enter your age: ")
        if not age.isdigit() or not (0 <= int(age) <= 120):
            print("Invalid age. Please enter a valid age.")
            continue
        
        dob = input("Enter your date of birth (DD/MM/YYYY): ")
        if not re.match(r"\d{2}/\d{2}/\d{4}", dob):
            print("Invalid date of birth. Please enter in the format DD/MM/YYYY.")
            continue
        
        try:
            dob_date = datetime.strptime(dob, "%d/%m/%Y")
            if dob_date > datetime.now():
                print("Invalid date of birth. Date cannot be in the future.")
                continue
        except ValueError:
            print("Invalid date of birth. Please enter a valid date.")
            continue
        
        phone = input("Enter your phone number: ")
        if not phone.isdigit() or len(phone) != 10:
            print("Invalid phone number. Please enter a 10-digit phone number.")
            continue
        
        break
    
    print(f"Name: {name}\nAge: {age}\nDate of Birth: {dob}\nPhone: {phone}")



validate_and_print_details()
