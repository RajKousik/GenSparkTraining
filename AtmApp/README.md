# ATM Management

## Project Code

- Frontend: [GitHub Repository](https://github.com/Thunder7Inc/WebAPI-Frontend)
- Backend: [GitHub Repository](https://github.com/Thunder7Inc/WebAPI)

## GIF Demo

![ATM Management System Output](./output_gif.gif)

You can view the full output video [here](https://github.com/RajKousik/Atm_Application/blob/master/atm_project_output.mp4)

## Frontend Overview

This repository contains the frontend code for the ATM Management System.

## Live Demo

[FrontEnd Demo](https://thunder7inc.github.io/WebAPI-Frontend/html/index.html)

## Features

- **Account Creation**: Allows users to create new accounts with a specified PIN.
- **Deposit**: Facilitates depositing money into existing accounts.
- **Withdrawal**: Allows withdrawals from existing accounts, following specified constraints.
- **Transaction History**: Displays a list of transactions with filtering options.

## Technologies Used

- **Frontend**: HTML, CSS, JavaScript
- **Libraries**: Bootstrap, jQuery, DataTables
- **Integration**: Consumes RESTful APIs provided by WebAPI backend.

## Getting Started

To get a local copy up and running, follow these steps:

1. Clone this repository.
2. Open `index.html` in your web browser.

## Usage

- Navigate through the links in the navigation bar to perform account operations.
- Use the transaction history page to view past transactions and apply filters.

## Backend Overview

This repository contains the backend code for the ATM Management System, providing APIs for handling transactions and accounts.

## Live Demo

[BackEnd Demo](https://thunderapi.azurewebsites.net/swagger/index.html)

## Technologies Used

- **Backend**: C#, .NET Web API
- **Database**: SQL Server (or specify the database technology used)
- **Integration**: Provides RESTful APIs consumed by the frontend.

## Setup

To set up the backend locally:

1. Clone this repository.
2. Set up your development environment.
3. Run the application locally using Visual Studio or `dotnet run`.

## API Endpoints

### Account Endpoint

- `GET /api/v1/Account/{accountId}` - Retrieves account details by account ID.
- `POST /api/v1/Account` - Creates a new account with provided details.

### Transaction Endpoint

- `POST /api/Transaction` - Performs a transaction (deposit/withdrawal).
- `GET /api/Transaction` - Retrieves all transactions.
- `GET /api/Transaction/{transactionId}` - Retrieves a transaction by ID.
- `GET /api/Transaction/account/{accountId}` - Retrieves transactions for a specific account.

## Contributors

- [HarshaVardhan](https://github.com/Thunder7Inc) - Handled Azure DevOps, Hosting, CI/CD Pipeline, Dockerization, Integrations.
- [Kousik Raj](https://github.com/RajKousik) - Backend Development
- [Mridula](https://github.com/mvmichuinternship) - Frontend Development
- [Mani Bharathi](https://github.com/itsmanibharathi) - Backend Development
- [Raghavendiran](https://github.com/Raghavendiran-2002) - Frontend Development

All contributors have participated in fixing issues and improving the overall functionality and reliability of the application.

## Contributing

Contributions are welcome! Feel free to fork this repository, make changes, and submit a pull request. Please follow the existing code style and conventions.
