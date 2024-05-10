using Microsoft.EntityFrameworkCore;
using RequestTrackerDALLibrary;
using RequestTrackerModelLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestTrackerDALLibrary
{
    public class RequestSolutionRepository : IRepository<int, RequestSolution>
    {
        private readonly RequestTrackerContext _context;
        public RequestSolutionRepository(RequestTrackerContext context)
        {
            _context = context;
        }

        public async Task<RequestSolution> Add(RequestSolution entity)
        {
            _context.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<RequestSolution> Delete(int key)
        {
            var requestSolution = await Get(key);
            if (requestSolution != null)
            {
                _context.RequestSolutions.Remove(requestSolution);
                await _context.SaveChangesAsync();
            }
            return requestSolution;
        }

        public async Task<RequestSolution> Get(int key)
        {
            var requestSolution = _context.RequestSolutions.SingleOrDefault(s => s.SolutionId == key);
            return requestSolution;
        }

        public async Task<IList<RequestSolution>> GetAll()
        {
            return await _context.RequestSolutions.ToListAsync();
        }

        public async Task<RequestSolution> Update(RequestSolution entity)
        {
            var requestSolution = await Get(entity.SolutionId);
            if (requestSolution != null)
            { 
                _context.RequestSolutions.Update(entity);
                int id =  _context.SaveChanges();
            }
            else
            {
                Console.Out.WriteLine("Request solution not found");
            }
            return entity;
        }
    }

}
