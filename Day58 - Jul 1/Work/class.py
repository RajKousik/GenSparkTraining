class BankAccount:
    """
    A class to represent a bank account.
    """

    # Class attribute
    bank_name = "National Bank"

    def __init__(self, owner: str, balance: float = 0.0):
        """
        Initialize the bank account with an owner and an initial balance.

        :param owner: Name of the account owner
        :param balance: Initial balance of the account, defaults to 0.0
        """
        self.owner = owner
        self.balance = balance

    def deposit(self, amount: float):
        """
        Deposit a specified amount into the account.

        :param amount: Amount to be deposited
        :raises ValueError: If the amount is not positive
        """
        if amount <= 0:
            raise ValueError("Deposit amount must be positive.")
        self.balance += amount
        print(f"{amount} has been deposited. New balance is {self.balance}.")

    def withdraw(self, amount: float):
        """
        Withdraw a specified amount from the account.

        :param amount: Amount to be withdrawn
        :raises ValueError: If the amount is not positive or if there are insufficient funds
        """
        if amount <= 0:
            raise ValueError("Withdrawal amount must be positive.")
        if amount > self.balance:
            raise ValueError("Insufficient funds.")
        self.balance -= amount
        print(f"{amount} has been withdrawn. New balance is {self.balance}.")

    def check_balance(self) -> float:
        """
        Check the current balance of the account.

        :return: Current balance
        """
        return self.balance

    def transfer(self, amount: float, target_account):
        """
        Transfer a specified amount to another account.

        :param amount: Amount to be transferred
        :param target_account: The target account to transfer to
        :raises ValueError: If the amount is not positive or if there are insufficient funds
        """
        if amount <= 0:
            raise ValueError("Transfer amount must be positive.")
        if amount > self.balance:
            raise ValueError("Insufficient funds.")
        self.withdraw(amount)
        target_account.deposit(amount)
        print(f"{amount} has been transferred to {target_account.owner}.")


account1 = BankAccount("Alice", 1000.0)
account2 = BankAccount("Bob", 500.0)

print(f"{account1.owner}'s initial balance: {account1.check_balance()}")
print(f"{account2.owner}'s initial balance: {account2.check_balance()}")

account1.deposit(200.0)
account1.withdraw(150.0)
account1.transfer(300.0, account2)

print(f"{account1.owner}'s final balance: {account1.check_balance()}")
print(f"{account2.owner}'s final balance: {account2.check_balance()}")
