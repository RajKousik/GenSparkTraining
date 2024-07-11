# Day 66

# ATM App Readme

## Introduction

This ATM app provides two main functionalities: Deposit and Withdrawal. It includes certain constraints to ensure proper usage and security.

## Features

1. **Deposit**
2. **Withdrawal**

## Output

![](./atm_output.gif)

## Constraints

1. One person cannot withdraw the amount if their account has less than the withdrawal amount.
2. One person cannot withdraw more than 10,000 at one go.
3. One person cannot deposit more than 20,000 at one go.

## Hosted Links

- **Frontend**: [ATM App Frontend](https://thunder7inc.github.io/WebAPI-Frontend/html/index.html)
- **Backend**: [ATM App Backend](https://thunderapi.azurewebsites.net/swagger/index.html)

## Technologies Used

- **Frontend**: HTML, CSS, JavaScript
- **Backend**: .NET Web API, C#

## Setup Instructions

### Prerequisites

- .NET SDK
- Visual Studio or any other C# IDE
- SQL Server

### Backend Setup

1. Clone the repository.
2. Open the solution in Visual Studio.
3. Configure the database connection string in `appsettings.json`.
4. Run the migrations to set up the database.
5. Start the application.

### Frontend Setup

1. Clone the repository.
2. Open the `index.html` file in a web browser to test locally.
3. For deployment, host the frontend files on a web server or GitHub Pages.

## API Endpoints

### Deposit

- **Endpoint**: `/api/v1/Account/deposit`
- **Method**: POST
- **Description**: Deposits a specified amount into the account.
- **Request Body**:
  ```json
  {
    "accountId": "string",
    "amount": "number",
    "pin": "number"
  }
  ```

### Withdrawal

- **Endpoint**: `/api/v1/Account/withdraw`
- **Method**: POST
- **Description**: Withdraws a specified amount from the account.
- **Request Body**:
  ```json
  {
    "accountId": "string",
    "amount": "number",
    "pin": "number"
  }
  ```

## Usage

1. Access the frontend application through the hosted link.
2. Use the deposit form to add money to your account, ensuring the deposit amount does not exceed 20,000.
3. Use the withdrawal form to take out money from your account, ensuring the withdrawal amount does not exceed 10,000 and that your account balance is sufficient.

## Contact

For any queries or issues, please reach out to the project maintainers.
