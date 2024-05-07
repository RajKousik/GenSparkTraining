# SQL Queries

## Topics Covered

* ERD Diagram

* Joins

* CTE (Common Table Expressions)

* Union, Union All

* Procedures

* Grant and Revoke

## Tasks

###  1. SQL Queries

Worked on Outer join queries in SQL. The file for the same can be found [here](./AssignmentQueries.sql)


1) Create a stored procedure that will take the author firstname and print all the books polished by him with the publisher's name

```
CREATE PROC proc_books_by_author(@author_fname VARCHAR(10))
AS
BEGIN
   SELECT t.title AS 'Book Title', p.pub_name AS 'Publisher''s Name'
   FROM titles t
   JOIN titleauthor ta ON ta.title_id = t.title_id
   JOIN authors a ON a.au_id = ta.au_id
   JOIN publishers p ON p.pub_id = t.pub_id
   WHERE a.au_fname = @author_fname
END

EXECUTE proc_books_by_author 'Michael'
```

2) Create a sp that will take the employee's firtname and print all the titles sold by him/her, price, quantity and the cost.

```
CREATE PROC proc_books_by_employee(@employee_fname VARCHAR(20))
AS
BEGIN
   SELECT t.title AS 'Book Title', t.price AS 'Price', s.qty AS 'Quantity', (t.price * s.qty) AS 'Total Cost'
   FROM Sales s
   JOIN titles t ON s.title_id = t.title_id
   JOIN employee e ON t.pub_id = e.pub_id
   WHERE e.fname = @employee_fname
END

EXEC proc_books_by_employee 'Paolo'
```


3) Create a query that will print all names from authors and employees

```
SELECT CONCAT(fname, ' ', lname) AS fullname
FROM Employee
UNION 
SELECT CONCAT(au_fname, ' ', au_lname) AS fullname
FROM authors
```


4) Create a  query that will float the data from sales, titles, publisher and authors table  to print title name, Publisher's name, author's full name with quantity ordered and price for the order for all orders print first 5 orders after sorting them based on the price of order

```
WITH OrderDataDetails_CTE
AS 
(
   SELECT t.title as 'Book Title', p.pub_name as 'Publisher Name', CONCAT(a.au_fname, ' ' ,a.au_lname) as 'Author Name', s.qty as 'Quantity', (t.price * s.qty) as 'Total Cost'
   FROM titles t
   JOIN publishers p ON p.pub_id = t.pub_id
   JOIN titleauthor ta ON ta.title_id = t.title_id
   JOIN authors a ON a.au_id = ta.au_id
   JOIN sales s ON s.title_id = t.title_id
)


SELECT TOP 5 * FROM OrderDataDetails_CTE ORDER BY [Total Cost] DESC;
```


5) GRANT AND REVOKE

```
CREATE USER Emilia without login;
GO

CREATE ROLE Queens;
GO

ALTER ROLE Queens ADD MEMBER Emilia; 
GO

GRANT SELECT ON dbo.authors TO Queens;
GO

EXECUTE AS USER = 'Emilia'
SELECT * FROM dbo.authors;

REVOKE SELECT ON dbo.authors TO Queens;
GO

REVERT;
```
