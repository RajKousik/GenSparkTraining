CREATE DATABASE RequestTrackerApp;
GO

use RequestTrackerApp;

CREATE TABLE Employees(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	Name VARCHAR(30),
	Age INT,
	DOB DATETIME,
	Salary FLOAT,
	Role VARCHAR(30),
)

CREATE TABLE Departments(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	Name VARCHAR(30),
	DepartmentHead INT,
)

CREATE TABLE Requests(
	Id INT PRIMARY KEY,
	RequestTest VARCHAR(50),
	RaisedBy INT,
	Status VARCHAR(50),
	ClosedBy INT,
	RaisedDate DATETIME,
	ClosedDate DATETIME
)


ALTER TABLE Employees
ADD EmployeeDepartment INT FOREIGN KEY REFERENCES Departments(Id);

sp_help Employees

SELECT * FROM Departments



DROP TABLE Departments;

UPDATE Departments 
SET Name = 'ECE'
WHERE ID = 4;