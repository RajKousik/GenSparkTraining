# DAY 21

## Topics

- Pair Programming On ERD

- Entity Framework Code First Approach

- Migrations 


## Work

**1. ERD Diagram**
- Creating a ER Diagram for the Pharmacy Management System
- Pair Programming with [Sugavanesh](https://github.com/sugan0tech-presidio)
- Driver - [Sugavanesh](https://github.com/sugan0tech-presidio/tasks)
- Navigator - [Kousik](https://github.com/RajKousik/GenSparkTraining)

![image](https://github.com/RajKousik/GenSparkTraining/assets/91744323/08980279-394a-49b1-9435-c605f1fdbaab)


This work can also be found [here](https://github.com/RajKousik/GenSparkTraining/blob/master/Day21/PhamacySolutionER.drawio.png)

**2. Code First Approach**

Implement code-first approach for the request tracker app.

The repository can be found [here](./RequestTrackerSolution)

```c#
using Microsoft.EntityFrameworkCore;
using RequestTrackerModelLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestTrackerDALLibrary
{
    public class RequestRepository : IRepository<int, Request>
    {
        private readonly RequestTrackerContext _context;
        public RequestRepository(RequestTrackerContext context)
        {
            _context = context;
        }
        public async Task<Request> Add(Request entity)
        {
            _context.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Request> Delete(int key)
        {
            var request = await Get(key);
            if (request != null)
            {
                _context.Requests.Remove(request);
                await _context.SaveChangesAsync();
            }
            return request;
        }

        public async Task<Request> Get(int key)
        {
            var request = _context.Requests.SingleOrDefault(r => r.RequestNumber == key);
            return request;
        }

        public async Task<IList<Request>> GetAll()
        {
            return await _context.Requests.ToListAsync();
        }

        public async Task<Request> Update(Request entity)
        {
            var request = await Get(entity.RequestNumber);
            if (request != null)
            {
                _context.Entry<Request>(request).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            return request;
        }
    }

}

```

**Output Screenshots**

