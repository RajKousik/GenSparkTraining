# Database Design and ER Modelling 

## Topics Covered

* SQL Queries

* Selection

* Projection

* Group By

* Group By Having

* Order By

* Filtering

* Rank By

## Tasks

### 1. Exercise 1

Complete the tasks in the given [website_link](https://pgexercises.com/questions/basic)

Completed SQL Screenshot output
![image](https://github.com/RajKousik/GenSparkTraining/assets/91744323/f5c35ef1-c681-4690-b324-b7978e5aa7a6)

### 2. Exercise 2

1) Print all the titles names
```
SELECT title FROM titles;
```
 
2) Print all the titles that have been published by 1389
```
SELECT title 
FROM titles 
WHERE pub_id = 1389;
```
 
3) Print the books that have price in rangeof 10 to 15
```
SELECT * FROM titles
WHERE price BETWEEN 10 AND 15;
```
 
4) Print those books that have no price
```
SELECT * FROM titles
WHERE price IS NULL;
```
 
5) Print the book names that strat with 'The'
```
SELECT * FROM titles
WHERE title LIKE 'The%';
```
 
6) Print the book names that do not have 'v' in their name
```
SELECT * FROM titles
WHERE NOT title LIKE '%v%';
```
 
7) print the books sorted by the royalty
```
SELECT * FROM titles
ORDER BY royalty;
```
 
8) print the books sorted by publisher in descending then by types in asending then by price in descending
```
SELECT * FROM titles 
ORDER BY pub_id DESC, 
type ASC, 
price DESC;
```
 
9) Print the average price of books in every type
```
SELECT type, AVG(Price) AS 'Average Price' 
FROM titles 
GROUP BY type;
```
 
10) print all the types in uniques
```
SELECT DISTINCT(type) 
FROM titles;
```

(OR) if the question is to print the type in which type was appeared only once

```
SELECT type
FROM titles
WHERE type NOT IN (
    SELECT type
    FROM titles
    GROUP BY type
    HAVING COUNT(*) > 1
);
```
 
11) Print the first 2 costliest books
```
SELECT TOP 2 * 
FROM titles 
ORDER BY price DESC;
```
 
12) Print books that are of type business and have price less than 20 which also have advance greater than 7000
```
SELECT * FROM titles 
WHERE type='business' 
and price < 20.00 
and advance > 7000;
```
 
13) Select those publisher id and number of books which have price between 15 to 25 and have 'It' in its name. Print only those which have count greater than 2. Also sort the result in ascending order of count
```
SELECT pub_id, COUNT(title_id) AS 'Number of books'
FROM titles 
WHERE price BETWEEN 15 AND 25 
AND title LIKE '%It%'
GROUP BY pub_id
HAVING COUNT(title_id) > 2
ORDER BY [Number of books] ASC;
```
 
14) Print the Authors who are from 'CA'
```
SELECT * FROM authors WHERE state = 'CA';
```
 
15) Print the count of authors from every state
```
SELECT state, 
COUNT(DISTINCT au_id) as 'Count of Authors' 
FROM authors 
GROUP BY state;
```
You can find the SQL file [here](https://github.com/RajKousik/GenSparkTraining/blob/master/Day15/SQL%20Queries/Day15%20Assignment%20SQL%20Query.sql)
