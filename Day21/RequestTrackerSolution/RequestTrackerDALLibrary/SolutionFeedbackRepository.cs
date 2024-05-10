using Microsoft.EntityFrameworkCore;
using RequestTrackerModelLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestTrackerDALLibrary
{
    public class SolutionFeedbackRepository : IRepository<int, SolutionFeedback>
    {
        protected readonly RequestTrackerContext _context;

        public SolutionFeedbackRepository(RequestTrackerContext context)
        {
            _context = context;
        }
        public async Task<SolutionFeedback> Add(SolutionFeedback entity)
        {
            _context.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<SolutionFeedback> Delete(int key)
        {
            var solutionFeedback = await Get(key);
            if (solutionFeedback != null)
            {
                _context.Remove(solutionFeedback);
                await _context.SaveChangesAsync();
            }
            return solutionFeedback;
        }

        public async virtual Task<SolutionFeedback> Get(int key)
        {
            var solutionFeedback = _context.SolutionFeedbacks.SingleOrDefault(s => s.FeedbackId == key);
            return solutionFeedback;
        }

        public async virtual Task<IList<SolutionFeedback>> GetAll()
        {
            return await _context.SolutionFeedbacks.ToListAsync();
        }

        public async Task<SolutionFeedback> Update(SolutionFeedback entity)
        {
            var solutionFeedback = await Get(entity.FeedbackId);
            if (solutionFeedback != null)
            {
                _context.Entry<SolutionFeedback>(solutionFeedback).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            return solutionFeedback;
        }
    }
}
