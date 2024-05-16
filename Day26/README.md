# Day 26
## Topics Covered

- ASP .Net Web API application

- Auto Mapping

- JWT Authentication

- SQL Queries Practice

## Work


**Pizza Shop API**

- Create an API that will allow user to login in a application that the user can order pizzas.(Sample Dominos/PizzaHut)

- Add an end-point that will list the pizza. List only those pizza's that are in stock.

- Device the model and DTOs as well.

- Add JWT authentication for the application

- Adding Authentication for Swagger

The repository for the assignment work can be found [here](../Day25/PizzaApplicationSolution)


## Technologies Used

- ASP.NET Core
- Entity Framework Core
- SQL Server
- Swagger for API documentation


### DEMO

![](./day26.gif)


## Hacker Rank

### A very Big Sum

In this challenge, you are required to calculate and print the sum of the elements in an array, keeping in mind that some of those integers may be quite large.

```c#
public static long aVeryBigSum(List<long> ar)
{
    long result = 0;
    foreach(var element in ar)
    {
        result += element;
    }
    return result;
}
```

You can find the Problem Statement [here](https://www.hackerrank.com/challenges/a-very-big-sum/problem?isFullScreen=true)
