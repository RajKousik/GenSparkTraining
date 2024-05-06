USE northwind;
GO

SELECT * FROM customers 
WHERE CustomerID NOT IN 
(
	SELECT DISTINCT customerid 
	FROM Orders
)

SELECT * FROM orders

SELECT ContactName,ShipName, ShipAddress
FROM customers c JOIN
orders o ON c.CustomerID = o.CustomerID;

SELECT ContactName, ShipName, ShipAddress
FROM customers c LEFT OUTER JOIN
orders o ON c.CustomerID = o.CustomerID;

SELECT * FROM products;
SELECT * FROM [order Details];

SELECT * FROM products 
WHERE productId NOT IN 
(
	SELECT DISTINCT ProductId 
	FROM [Order Details]
)