using EmployeeRequestTrackerAPI.Contexts;
using EmployeeRequestTrackerAPI.Exceptions;
using EmployeeRequestTrackerAPI.Interfaces;
using EmployeeRequestTrackerAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeRequestTrackerAPI.Repositories
{
    public class RequestRepository : IRepository<int, Request>
    {
        private readonly RequestTrackerContext _context;
        public RequestRepository(RequestTrackerContext context)
        {
            _context = context;
        }
        public async Task<Request> Add(Request item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<Request> Delete(int key)
        {
            var request = await Get(key);
            if (request != null)
            {
                var removedRequest = _context.Remove(request);
                await _context.SaveChangesAsync();
                return request;
            }
            throw new NoSuchRequestException();
        }

        public async Task<Request> Get(int key)
        {
            var request = await _context.Requests.Include(r => r.RequestRaisedByEmployee)
                                                 .Include(r => r.RequestClosedByEmployee)
                                                 .FirstOrDefaultAsync(r => r.Id == key);
            return request;
        }

        public async Task<IEnumerable<Request>> Get()
        {
            var requests = await _context.Requests.ToListAsync();
            return requests;

        }

        public async Task<Request> Update(Request item)
        {
            var request = await Get(item.Id);
            if (request != null)
            {
                _context.Update(item);
                await _context.SaveChangesAsync();
                return request;
            }
            throw new NoSuchRequestException();
        }
    }
}
