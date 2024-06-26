USE Pubs;
GO

SELECT * FROM authors;
SELECT * FROM publishers;
SELECT * FROM titles;
SELECT * FROM Employee;
SELECT * FROM Sales;

-- 1. Create a stored procedure that will take the author firstname 
-- and print all the books polished by him with the publisher's name

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


-- 2. Create a sp that will take the employee's firtname and print all the titles
-- sold by him/her, price, quantity and the cost.

ALTER PROC proc_books_by_employee(@employee_fname VARCHAR(20))
AS
BEGIN
   SELECT t.title AS 'Book Title', sum(t.price) AS 'Price', sum(s.qty) AS 'Quantity', sum(t.price) * sum(s.qty) AS 'Total Cost' 
   FROM Employee e
   JOIN titles t ON t.pub_id = e.pub_id
   JOIN Sales s ON t.title_id = s.title_id
   WHERE e.fname = @employee_fname
   GROUP BY t.title
   ORDER BY [Total Cost] DESC
END

EXEC proc_books_by_employee 'Paolo'

-- 3. Create a query that will print all names from authors and employees

SELECT CONCAT(fname, ' ', lname) AS fullname
FROM Employee
UNION 
SELECT CONCAT(au_fname, ' ', au_lname) AS fullname
FROM authors


-- 4. Create a  query that will float the data from sales, titles, publisher and authors table 
-- to print title name, Publisher's name, author's full name with quantity ordered and price for the order for all orders
-- print first 5 orders after sorting them based on the price of order

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


-- 5. GRANT AND REVOKE


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

