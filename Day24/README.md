# DAY 24

## Topics

- Web API - Employee Request Controller

- Migrations

- RESTFUL Service

- 3 Tier Architecture


## Work

- 1. ClinicAPI

- 2. SQL Queries

### 1. ClinicAPI

To Implement a ClinicAPI, a web API that provides endpoints for managing doctors in a clinic. The API allows clients to list doctors, update a doctor's experience, and list doctors based on their specialty.

You can find the repository [here](./ClinicManagementSolution)

### Endpoints

- List Doctors

- Update Doctor Experience

- List Doctors by Specialty

### Project Structure

The project is structured as follows:

- **ClinicManagementAPI** : Contains the main ASP.NET Core Web API project.

    - **Controllers**: Contains the API controllers for handling HTTP requests.
    - **Exceptions**: Contains custom exception classes for error handling.
    - **Interfaces**: Contains interfaces defining contracts for services and repositories.
    - **Models**: Contains model classes representing entities in the application.
    - **Repositories**: Contains classes implementing data access logic.
    - **Services**: Contains classes implementing business logic.


### Output 

![image](./day24.gif)


### Code Segment

```c#

using ClinicManagementAPI.Exceptions;
using ClinicManagementAPI.Interfaces;
using ClinicManagementAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _doctorService;

        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        [HttpGet]
        public async Task<ActionResult<IList<Doctor>>> Get()
        {
            try
            {
                var doctors = await _doctorService.GetDoctors();
                return Ok(doctors.ToList());
            }
            catch (NoDoctorsFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<IList<Doctor>>> AddDoctor([FromBody]Doctor doctor)
        {
            try
            {
                var result = await _doctorService.AddDoctor(doctor);
                return Ok(result);
            }
            catch (NoSuchDoctorException ex)
            {
                return NotFound(ex.Message);
            }
        }


        [HttpPut]
        public async Task<ActionResult<Doctor>> UpdateExperience(int id, [FromBody]int experience)
        {
            try
            {
                var doctor = await _doctorService.UpdateDoctorExperience(id, experience);
                return Ok(doctor);
            }
            catch (NoSuchDoctorException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetDoctorBySpeciality")]
        public async Task<ActionResult<IList<Doctor>>> GetBySpeciality(string speciality)
        {
            try
            {
                var doctors = await _doctorService.GetDoctorBySpeciality(speciality);
                return Ok(doctors);
            }
            catch (NoDoctorsFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}


```

### 2. SQL Queries

- **Higher Than 75 Marks**
```sql
SELECT Name
FROM STUDENTS 
WHERE Marks > 75
ORDER BY RIGHT(Name, 3), Id;
```
Problem Statement can be found [here](https://www.hackerrank.com/challenges/more-than-75-marks/problem?isFullScreen=true)

- **Contest Leaderboard**
```sql
with cte_data AS
(
    SELECT hacker_id, challenge_id, MAX(score) AS max_score 
    FROM Submissions 
    GROUP BY hacker_id, challenge_id
    HAVING MAX(score) > 0
)

SELECT h.hacker_id, h.name, SUM(c.max_score) AS total_max_score
FROM Hackers h 
JOIN cte_data c ON h.hacker_id = c.hacker_id
GROUP BY h.hacker_id, h.name
ORDER BY total_max_score DESC, h.hacker_id ASC;
```
Problem Statement can be found [here](https://www.hackerrank.com/challenges/contest-leaderboard/problem?isFullScreen=true)
