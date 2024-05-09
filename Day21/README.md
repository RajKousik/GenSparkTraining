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

![image](https://github.com/RajKousik/GenSparkTraining/assets/91744323/01b618c3-65b3-480d-8d7b-74c48f965869)

![image](https://github.com/RajKousik/GenSparkTraining/assets/91744323/40e28ffd-c057-4167-88ee-9c707d6b261f)

![image](https://github.com/RajKousik/GenSparkTraining/assets/91744323/edc7fb56-bac4-4059-a32e-6e0df9f52472)

![image](https://github.com/RajKousik/GenSparkTraining/assets/91744323/65b8a889-81a0-49b6-a075-a344d673187c)

![image](https://github.com/RajKousik/GenSparkTraining/assets/91744323/0c4b61de-caf4-4284-a3cb-8513bd0960d7)


