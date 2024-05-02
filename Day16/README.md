# SQL Queries 

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

1. Retrieve everything from a table
```
SELECT * FROM cd.facilities;
```

2. Retrieve specific columns from a table
```
SELECT name, membercost
FROM cd.facilities;
```

3. Control which rows are retrieved
```
SELECT *
FROM cd.facilities
WHERE membercost > 0;
```

4. Control which rows are retrieved - part 2
```
SELECT facid, name, membercost, monthlymaintenance
FROM cd.facilities
WHERE
membercost > 0 AND membercost < monthlymaintenance / 50;
```

5. Basic string searches
```
SELECT *
FROM cd.facilities
WHERE name LIKE '%Tennis%';
```

6. Matching against multiple possible values
```
SELECT *
FROM cd.facilities
WHERE facid IN (1, 5);
```

7. Classify results into buckets
```
SELECT
name,
CASE
    WHEN monthlymaintenance > 100 THEN 'expensive'
    ELSE 'cheap'
END AS "cost"
FROM
cd.facilities;
```

8. Working with dates
```
SELECT memid, surname, firstname, joindate
FROM cd.members
WHERE joindate >= '2012-09-01';
```

9. Removing duplicates, and ordering results
```
SELECT DISTINCT surname
FROM cd.members
ORDER BY surname
LIMIT 10;
```

10. Combining results from multiple queries
```
SELECT surname FROM cd.members
UNION DISTINCT
SELECT name FROM cd.facilities;
```

11. Simple aggregation
```
SELECT MAX(joindate) AS latest FROM cd.members;
```

12. More aggregation
```
SELECT firstname, surname, joindate
FROM cd.members
WHERE joindate = (
  SELECT MAX(joindate)
  FROM cd.members
);
```


#### Completed SQL Screenshot output:

![image](https://github.com/RajKousik/GenSparkTraining/blob/master/Day16/SQL%20Queries/Postgress%20SQL%20Assignment.png)

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
You can find the SQL file [here](https://github.com/RajKousik/GenSparkTraining/blob/master/Day16/SQL%20Queries/SQL%20Pubs%20DB%20-%20Assignment%20Day%2016.sql)
