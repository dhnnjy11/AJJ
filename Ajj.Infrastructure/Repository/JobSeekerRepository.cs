using Ajj.Core.Entities;
using Ajj.Core.Interface;
using Ajj.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ajj.Infrastructure.Repository
{
    public class JobSeekerRepository : Repository<JobSeeker>, IJobSeekerRepository
    {
        public JobSeekerRepository(ApplicationDbContext context) : base(context)
        {
        }

        public new JobSeeker GetById(int id)
        {
            return _context.jobseekers.Where(x => x.Id == id)
                .Include(x=>x.ApplicationUser)
                .Include(x => x.VisaCategory)
                .SingleAsync()
                .Result;
        }

        public async new Task<ICollection<JobSeeker>> GetAllAsyn()
        {
            return await _context.jobseekers
                .Include(user => user.ApplicationUser)
                .Include(pref => pref.Province)
                .ToListAsync();
        }

        public async Task<IEnumerable<int>> GetPreferencesAsync(int jobseekerId)
        {
            return await _context.jobskills
                          .Include(x => x.JobSeeker)
                          .Include(x => x.BusinessStream)
                          .Where(x => x.JobSeekerId == jobseekerId) 
                          .Select(x => x.BusinessStreamId)
                          .ToListAsync();
        }

        public JobSeeker GetJobSeekerByUserId(string applicationId)
        {
            return _context.jobseekers.Where(x => x.ApplicationUserId == applicationId)
                           .Include(x => x.VisaCategory)
                           .Include(x => x.Country)
                           .Include(x => x.Province)
                           .Include(x => x.JobSkills)
                           .FirstOrDefault();
        }
    }
}