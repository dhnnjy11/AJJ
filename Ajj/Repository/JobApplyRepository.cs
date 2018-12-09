using Ajj.Data;
using Ajj.Interface;
using Ajj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ajj.Repository
{
    public class JobApplyRepository : Repository<JobApply>, IJobApplyRepository
    {
        public JobApplyRepository(ApplicationDbContext context) : base(context)
        {
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

    
        public int TotalJobAppliedTodayByCandidate(string UserId)
        {
            var jobAppliesToday = _context.jobapplies.Where(x => x.UserID == UserId && x.ApplyDate.AddHours(9).Day == DateTime.Today.Day);
            var coutner = jobAppliesToday.Count();
            return coutner;
        }



        public void SaveUserJob(Job job)
        {
            //JobApply jobApply = new JobApply();
            //jobApply.Job = job;
            //jobApply.ApplicationUser = _context.Add(jobApply);
            throw new NotImplementedException();
        }


    }
}
