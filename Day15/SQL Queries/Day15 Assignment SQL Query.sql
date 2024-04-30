CREATE database EmployeeDBApr30;
GO

DROP DATABASE EmployeeDBApr30;

USE EmployeeDBApr30
GO

CREATE TABLE Emp (
    empno INT PRIMARY KEY,
    empname VARCHAR(100),
    salary DECIMAL(10, 2),
);

CREATE TABLE Department (
    deptname VARCHAR(100) PRIMARY KEY,
    floor INT,
    phone VARCHAR(20),
    empno INT NOT NULL constraint fk_empno REFERENCES Emp(empno)
);

DROP TABLE Department;




CREATE TABLE Item (
    itemname VARCHAR(100) PRIMARY KEY,
    itemtype VARCHAR(100),
    itemcolor VARCHAR(100)
);

CREATE TABLE Sales (
    salesno INT PRIMARY KEY,
    saleqty INT,
    deptname VARCHAR(100) NOT NULL constraint fk_deptname REFERENCES DEPARTMENT(deptname),
	itemname VARCHAR(100) NOT NULL constraint fk_sales_itemname REFERENCES Item(itemname)
);

-- Droped and created again
DROP TABLE Sales;

-- Droped and created again
DROP TABLE ITEM;

ALTER TABLE Emp 
ADD deptname VARCHAR(100) CONSTRAINT fk_emp_deptname REFERENCES DEPARTMENT(deptname);

ALTER TABLE Emp 
ADD bossno INT CONSTRAINT fk_emp_bossno REFERENCES Emp(empno);

sp_help Sales;

INSERT INTO Item (itemname, itemtype, itemcolor)
VALUES
    ('Pocket Knife-Nile', 'E', 'Brown'),
    ('Pocket Knife-Avon', 'E', 'Brown'),
    ('Compass', 'N', NULL),
    ('Geo positioning system', 'N', NULL),
    ('Elephant Polo stick', 'R', 'Bamboo'),
    ('Camel Saddle', 'R', 'Brown'),
    ('Sextant', 'N', NULL),
    ('Map Measure', 'N', NULL),
    ('Boots-snake proof', 'C', 'Green'),
    ('Pith Helmet', 'C', 'Khaki'),
    ('Hat-polar Explorer', 'C', 'White'),
    ('Exploring in 10 Easy Lessons', 'B', NULL),
    ('Hammock', 'F', 'Khaki'),
    ('How to win Foreign Friends', 'B', NULL),
    ('Map case', 'E', 'Brown'),
    ('Safari Chair', 'F', 'Khaki'),
    ('Safari cooking kit', 'F', 'Khaki'),
    ('Stetson', 'C', 'Black'),
    ('Tent - 2 person', 'F', 'Khaki'),
    ('Tent -8 person', 'F', 'Khaki');


-- Insert data into EMP table
INSERT INTO Emp (empno, empname, salary, bossno)
VALUES
    (1, 'Alice', 75000, NULL),
    (2, 'Ned', 45000, 1),
    (3, 'Andrew', 25000, 2),
    (4, 'Clare', 22000, 2),
    (5, 'Todd', 38000, 1),
    (6, 'Nancy', 22000, 5),
    (7, 'Brier', 43000, 1),
    (8, 'Sarah', 56000, 7),
    (9, 'Sophile', 35000, 1),
    (10, 'Sanjay', 15000, 3),
    (11, 'Rita', 15000, 4),
    (12, 'Gigi', 16000, 4),
    (13, 'Maggie', 11000, 4),
    (14, 'Paul', 15000, 3),
    (15, 'James', 15000, 3),
    (16, 'Pat', 15000, 3),
    (17, 'Mark', 15000, 3);

SELECT * FROM Emp;

-- Insert data into DEPARTMENT table
INSERT INTO Department (deptname, floor, phone, empno)
VALUES
    ('Management', 5, 34, 1),
    ('Books', 1, 81, 4),
    ('Clothes', 2, 24, 4),
    ('Equipment', 3, 57, 3),
    ('Furniture', 4, 14, 3),
    ('Navigation', 1, 41, 3),
    ('Recreation', 2, 29, 4),
    ('Accounting', 5, 35, 5),
    ('Purchasing', 5, 36, 7),
    ('Personnel', 5, 37, 9),
    ('Marketing', 5, 38, 2);

INSERT INTO SALES (salesno, saleqty, itemname, deptname)
VALUES
    (101, 2, 'Boots-snake proof', 'Clothes'),
    (102, 1, 'Pith Helmet', 'Clothes'),
    (103, 1, 'Sextant', 'Navigation'),
    (104, 3, 'Hat-polar Explorer', 'Clothes'),
    (105, 5, 'Pith Helmet', 'Equipment'),
    (106, 2, 'Pocket Knife-Nile', 'Clothes'),
    (107, 3, 'Pocket Knife-Nile', 'Recreation'),
    (108, 1, 'Compass', 'Navigation'),
    (109, 2, 'Geo positioning system', 'Navigation'),
    (110, 5, 'Map Measure', 'Navigation'),
    (111, 1, 'Geo positioning system', 'Books'),
    (112, 1, 'Sextant', 'Books'),
    (113, 3, 'Pocket Knife-Nile', 'Books'),
    (114, 1, 'Pocket Knife-Nile', 'Navigation'),
    (115, 1, 'Pocket Knife-Nile', 'Equipment'),
    (116, 1, 'Sextant', 'Clothes'),
    (117, 1, 'Sextant', 'Equipment'),
    (118, 1, 'Sextant', 'Recreation'),
    (119, 1, 'Sextant', 'Furniture'),
    (120, 1, 'Pocket Knife-Nile', 'Furniture'),
    (121, 1, 'Exploring in 10 easy lessons', 'Books'),
    (122, 1, 'How to win foreign friends', 'Books'),
    (123, 1, 'Compass', 'Books'),
    (124, 1, 'Pith Helmet', 'Books'),
    (125, 1, 'Elephant Polo stick', 'Recreation'),
    (126, 1, 'Camel Saddle', 'Recreation');

UPDATE Emp 
SET deptname = 'Management' WHERE Empno in (1); 

UPDATE Emp 
SET deptname = 'Marketing' WHERE Empno in (2,3,4); 

UPDATE Emp 
SET deptname = 'Accounting' WHERE Empno in (5, 6); 

UPDATE Emp 
SET deptname = 'Purchasing' WHERE Empno in (7, 8); 

UPDATE Emp 
SET deptname = 'Personnel' WHERE Empno in (9); 

UPDATE Emp 
SET deptname = 'Navigation' WHERE Empno in (10); 

UPDATE Emp 
SET deptname = 'Books' WHERE Empno in (11); 

UPDATE Emp 
SET deptname = 'Clothes' WHERE Empno in (12, 13);

UPDATE Emp 
SET deptname = 'Equipment' WHERE Empno in (14, 15);

UPDATE Emp 
SET deptname = 'Furniture' WHERE Empno in (16);

UPDATE Emp 
SET deptname = 'Recreation' WHERE Empno in (17);

SELECT * from Emp;
SELECT * from Department;
SELECT * from Sales;
SELECT * from Item;