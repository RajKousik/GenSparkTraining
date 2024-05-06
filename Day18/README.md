# Database Design and ER Modelling 

## Topics Covered

* SQL

* ERD Diagram

* Entity

* Attributes

* Outer Join

## Tasks

###  Database Design for a Shop

Design a database for a shop that sells products. The database should consider the following points:

* A shop want to record the stock available and billed in a shop.

* Customer data has to be minimum.

* One product could be supplied by multiple supplier.

* A Single supplier can supply many products.


### ERD Design

Customer - Id(pk), Name, Phone, Email

Product - Id(pk), Name, Price, QuantityInHand

SupplierMaster - Id(pk), Name, Phone

ProductSupplier - Id(pk), ProductId(fk), SupplierId(fk)

Order - OrderId, ProductId, QuantityOrdered, Price

OrderMaster - OrderId,customerId, Date of Order, TotalAmount.

![ERD](./Shop_ERD.jpg)


## SQL QUeries

Worked on Outer join queries in SQL. The file for the same can be found [here](./OuterJoinQueries.sql)
