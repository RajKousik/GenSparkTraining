use pubs;
Go

SELECT * FROM sales;
SELECT * FROM Stores;
SELECT * FROM Titles;
SELECT * FROM publishers;
SELECT * FROM authors;
SELECT * FROM titleauthor;
SELECT * FROM employee;

--1) Print the storeid and number of orders for the store
SELECT s.stor_id, COUNT(*) AS num_orders
FROM sales s
JOIN stores st ON s.stor_id = st.stor_id
GROUP BY s.stor_id;

--2) print the numebr of orders for every title
SELECT t.title, COUNT(s.title_id) AS num_orders
FROM titles t
JOIN sales s ON t.title_id = s.title_id
GROUP BY t.title;

--3) print the publisher name and book name
SELECT p.pub_name, t.title 
FROM publishers p
JOIN titles t ON p.pub_id = t.pub_id

--4) Print the author full name for all the authors
SELECT CONCAT(au_fname, ' ', au_lname) AS 'Author Full Name'
FROM authors 

--5) Print the price or every book with tax (price+price*12.36/100)
SELECT title, price, price + (price * 12.36 / 100) AS 'price with tax'
FROM titles;

--6) Print the author name, title name
SELECT CONCAT(a.au_fname, ' ', a.au_lname) AS 'Author Full Name', t.title
FROM authors a
JOIN titleauthor ta ON a.au_id = ta.au_id
JOIN titles t ON ta.title_id = t.title_id 

--7) print the author name, title name and the publisher name
SELECT CONCAT(a.au_fname, ' ', a.au_lname) AS 'Author Full Name', t.title, p.pub_name AS 'Publisher Name'
FROM authors a
JOIN titleauthor ta ON a.au_id = ta.au_id
JOIN titles t ON ta.title_id = t.title_id 
JOIN publishers p ON t.pub_id = p.pub_id

--8) Print the average price of books pulished by every publisher
SELECT p.pub_name, AVG(t.price) AS 'Average Price'
FROM titles t
JOIN publishers p ON p.pub_id = t.pub_id
GROUP BY p.pub_name;

--9) print the books published by 'Marjorie'
SELECT t.title
FROM titles t
JOIN titleauthor ta ON t.title_id = ta.title_id
JOIN authors a ON ta.au_id = a.au_id
WHERE a.au_fname = 'Marjorie' OR a.au_lname = 'Marjorie'

--10) Print the order numbers of books published by 'New Moon Books'
SELECT s.ord_num
FROM sales s
JOIN titles t ON s.title_id = t.title_id
JOIN publishers p ON t.pub_id = p.pub_id
WHERE p.pub_name = 'New Moon Books';

--11) Print the number of orders for every publisher
SELECT t.pub_id, p.pub_name, COUNT(s.ord_num) AS 'Number of Books Published'
FROM sales s
JOIN titles t ON s.title_id = t.title_id
JOIN publishers p ON t.pub_id = p.pub_id
GROUP BY t.pub_id, p.pub_name

--12) print the order number , book name, quantity, price and the total price for all orders
SELECT s.ord_num, t.title, s.qty, t.price, s.qty * t.price AS 'Total Price'
FROM sales s
JOIN titles t ON s.title_id = t.title_id

--13) print the total order quantity for every book
SELECT t.title, SUM(s.qty) AS 'Total No of Orders'
FROM sales s
JOIN titles t ON s.title_id = t.title_id
GROUP BY t.title

--14) print the total ordervalue for every book
SELECT t.title, SUM(s.qty * t.price) AS 'Total Order Value'
FROM sales s
JOIN titles t ON s.title_id = t.title_id
GROUP BY t.title

--15) print the orders that are for the books published by the publisher for which 'Paolo' works for
SELECT s.ord_num
FROM sales s
JOIN titles t ON s.title_id = t.title_id
JOIN employee e ON t.pub_id = e.pub_id
WHERE e.fname = 'Paolo';

