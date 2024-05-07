USE NorthWind;
GO

SELECT * FROM Categories;
UNION
SELECT * FROM Suppliers;

SELECT CategoryID, CategoryName FROM Categories
UNION
SELECT SupplierId, CompanyName FROM Suppliers;

SELECT * FROM Orders WHERE ShipCountry='Spain'
INTERSECT
SELECT * FROM orders WHERE Freight>50

SELECT TOP 5 * FROM orders ORDER BY Freight DESC

--get the order id, productname and quantitysold of products that have price
--greater than 15

SELECT od.OrderID, p.ProductName, od.Quantity as 'Quantity Sold', p.UnitPrice
FROM [Order Details] od
JOIN Products p ON od.ProductID = p.productId
WHERE p.UnitPrice > 15


--get the order id, productname and quantitysold of products that are from category 'dairy'
--and within the price range of 10 to 20

SELECT od.OrderID, p.ProductName, od.Quantity as 'Quantity Sold', p.UnitPrice
FROM [Order Details] od
JOIN Products p ON od.ProductID = p.productId
LEFT JOIN Categories c ON p.CategoryID = c.CategoryID
WHERE c.CategoryName LIKE '%Dairy%' 
AND 
p.UnitPrice BETWEEN 10 AND 20
ORDER BY p.UnitPrice


-------------

SELECT od.OrderID, p.ProductName, od.Quantity as 'Quantity Sold', p.UnitPrice
FROM [Order Details] od
JOIN Products p ON od.ProductID = p.productId
WHERE p.UnitPrice > 15

UNION

SELECT od.OrderID, p.ProductName, od.Quantity as 'Quantity Sold', p.UnitPrice
FROM [Order Details] od
JOIN Products p ON od.ProductID = p.productId
LEFT JOIN Categories c ON p.CategoryID = c.CategoryID
WHERE c.CategoryName LIKE '%Dairy%' 
AND 
p.UnitPrice BETWEEN 10 AND 20
ORDER BY p.UnitPrice DESC


with OrderDetails_CTE(OrderID,ProductName,Quantity,Price)
as
(select OrderID, ProductName, Quantity "Quantity Sold",p.UnitPrice
from [Order Details] od join Products p
on od.ProductId = p.ProductID
where p.UnitPrice>15
union
SELECT OrderID, p.productname, Quantity "Quantity Sold", p.UnitPrice FROM [Order Details]
JOIN Products p ON p.ProductID = [Order Details].ProductID
JOIN Categories c ON c.CategoryID = p.CategoryID
WHERE p.UnitPrice BETWEEN 10 AND 20 AND c.CategoryName LIKE '%Dairy%')

SELECT * FROM OrderDetails_CTE ORDER BY PRICE DESC;

create view vwOrderDetails
as
(select OrderID, ProductName, Quantity "Quantity Sold",p.UnitPrice
from [Order Details] od join Products p
on od.ProductId = p.ProductID
where p.UnitPrice>15
union
SELECT OrderID, p.productname, Quantity "Quantity Sold", p.UnitPrice FROM [Order Details]
JOIN Products p ON p.ProductID = [Order Details].ProductID
JOIN Categories c ON c.CategoryID = p.CategoryID
WHERE p.UnitPrice BETWEEN 10 AND 20 AND c.CategoryName LIKE '%Dairy%')

SELECT * FROM vwOrderDetails ORDER BY UnitPrice DESC;

SELECT * FROM [Orders]
SELECT * FROM Customers


WITH OrderDetails_CTE 
as
(

	SELECT od.OrderID, c.ContactName, p.ProductName
	FROM [Order Details] od
	JOIN Products p ON od.ProductID = p.productId
	JOIN Orders o ON o.OrderID = od.OrderID
	JOIN Customers c on c.CustomerID = o.CustomerID
	WHERE c.Country = 'USA'
	
	UNION

	SELECT od.OrderID, c.ContactName, p.ProductName
	FROM [Order Details] od
	JOIN Products p ON od.ProductID = p.productId
	JOIN Orders o ON o.OrderID = od.OrderID
	JOIN Customers c on c.CustomerID = o.CustomerID
	WHERE c.Country = 'FRANCE'
	AND o.Freight < 20

	
)

SELECT TOP 10 * FROM OrderDetails_CTE;




Select * from Categories where Description like '%a%';

sp_help Categories


create index idxEmpEmail on Employees(email)
select * from employees where email like 'r%'
sp_help Employees
drop index idxEmpEmail on Employees


-- PROCEDURES

create procedure proc_FirstProcedure
as
begin
    print 'Hello'
end

execute proc_FirstProcedure

create proc proc_GreetWithName(@greetMsg varchar(20), @cname varchar(20))
as
begin
   print @greetMsg + ' ' + @cname
end

exec proc_GreetWithName 'Hola', 'Raj RK'

alter proc proc_GreetWithName(@greetMsg varchar(20), @cname varchar(20))
as
begin
   print @greetMsg + ' ' + @cname + ' Dracarys'
end

exec proc_GreetWithName 'Hola', 'Raj RK'



alter proc proc_GreetWithName(@greetMsg varchar(20), @cname varchar(20), @favNo int)
as
begin
   print @greetMsg + ' ' + @cname + ' Dracarys' + ' No is ' + cast(@favNo AS varchar)
end

exec proc_GreetWithName 'Hola', 'Raj RK', 7

DROP PROCEDURE proc_GreetWithName


create proc proc_AddEmployee(@ename varchar(10),@edob datetime,
@earea varchar(10), @ephone varchar(15), @eemail varchar(15))
as
begin
   insert into Employees(name,DateOfBirth,EmployeeArea,Phone,Email)
   values(@ename,@edob,@earea,@ephone,@eemail)
end

SELECT * FROM Employees;

exec proc_AddEmployee 'Raj','2003-03-13','FFFF','8427873421','raj@gmail.com'


/*CREATE USER 'dummy'@'localhost' identifed by 'password';

GRANT EXECUTE ON proc_AddEmployee TO TesterRole WITH GRANT OPTION;

SELECT * FROM sys.database_principals
*/

