using Ajj.Core.Entities;
using Ajj.Core.Interface;
using Ajj.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ajj.Infrastructure.Repository
{
    public class JobApplyRepository : Repository<JobApply>, IJobApplyRepository
    {
        public JobApplyRepository(ApplicationDbContext context) : base(context)
        {
        }

        public new IQueryable GetAll()
        {
            return _context.jobapplies
                .Include(user => user.User)
                .Include(job => job.Job)
                    .ThenInclude(client=>client.Client);
        }

        public async new Task<ICollection<JobApply>> GetAllAsyn()
        {
            return await _context.jobapplies
                .Include(user => user.User)
                .Include(job => job.Job)                    
                    .ThenInclude(client => client.Client)
                .Include(job => job.Job)
                    .ThenInclude(job=>job.JobCategory)
                .ToListAsync();
        }

        /// <summary>
        /// For Counting applicant for particular job
        /// </summary>
        /// <param name="jobId">Job Id</param>
        /// <returns>List of Job Apply Object</returns>
        public List<JobApply> GetAppliedCount(long jobId)
        {
            var jobapplies = _context.jobapplies.Where(x => x.JobID == jobId);
            return jobapplies.ToList();
        }

        public bool IsAlreadyApplied(string userId, long jobId)
        {
            bool result = false;
            var jobapplies = _context.jobapplies.Where(x => x.JobID == jobId && x.UserID == userId);
            if(jobapplies.Count() > 0)
            {
                result = true;
            }
            return result;
        }

        [Obsolete("use method JobAppliedCount instead")]
        public int TotalJobAppliedTodayByCandidate(string UserId)
        {
            var jobAppliesToday = _context.jobapplies.Where(x => x.UserID == UserId && x.ApplyDate.Day == DateTime.Today.Day);
            var coutner = jobAppliesToday.Count();
            return coutner;
        }

        public int JobAppliedCount(ApplicationUser user)
        {
            var jobAppliesToday = _context.jobapplies.Where(x => x.UserID == user.Id && x.ApplyDate.Day == DateTime.Today.Day);
            var coutner = jobAppliesToday.Count();
            return coutner;
        }

        public void SaveUserJob(Job job)
        {
            throw new NotImplementedException();
        }
    }
}