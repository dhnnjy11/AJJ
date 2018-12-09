using Ajj.Data;
using Ajj.Interface;
using Ajj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ajj.Repository
{
    public class JobSeekerRepository : Repository<JobSeeker>, IJobSeekerRepository
    {
        public JobSeekerRepository(ApplicationDbContext context) : base(context)
        {
        }

        public JobSeeker GetJobSeekerByUserId(string applicationId)
        {
            return _context.jobseekers.First();

        }

        //public new int Update(JobSeeker entity)
        //{
           
           

        //    return _context.SaveChanges();
        //}
    }
}
