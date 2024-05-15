# Day 25

## Topics Covered

- ASP .Net Web API application

- DTO

- Password Hashing

- Attributes and other good coding principles

## Work

### Question 1

**Weather Observation Station 5**

This is a hackerrank problem. The problem statement for the same can be found [here](https://www.hackerrank.com/challenges/weather-observation-station-5/problem?isFullScreen=true)

```sql
SELECT TOP 1 city, LEN(city)
FROM station 
ORDER BY LEN(city), city;
SELECT TOP 1 city, LEN(city)
FROM station 
ORDER BY LEN(city) DESC, city;
```

### Question 2

**Ollivander's Inventory**

This is a hackerrank problem. The problem statement for the same can be found [here](https://www.hackerrank.com/challenges/harry-potter-and-wands/problem?isFullScreen=true)

```sql
WITH CTE_DATA AS (
    SELECT W.id, WP.age, W.coins_needed, W.power, 
    ROW_NUMBER() OVER (PARTITION BY W.power, WP.age ORDER BY W.coins_needed) AS             cost_rank FROM 
    Wands W
    JOIN Wands_Property WP ON W.code = WP.code
    WHERE WP.is_evil = 0
)
SELECT id, age, coins_needed, power
FROM CTE_DATA
WHERE cost_rank = 1
ORDER BY power DESC, age DESC;
```

### Question 3

**Pizza Shop API**

- Create an API that will allow user to login in a application that the user can order pizzas.(Sample Dominos/PizzaHut)

- Add an end-point that will list the pizza. List only those pizza's that are in stock.

- Device the model and DTOs as well.

The repository for the assignment work can be found [here](./PizzaApplicationSolution)


## Technologies Used

- ASP.NET Core
- Entity Framework Core
- SQL Server
- Swagger for API documentation


### DEMO

![](./day25.gif)


</details>