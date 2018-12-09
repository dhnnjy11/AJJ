using Ajj.Data;
using Ajj.Interface;
using Ajj.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Ajj.Repository
{
    public class JobRepository : Repository<Job>, IJobRepository
    {
        public JobRepository(ApplicationDbContext context) : base(context)
        {
        }

        public IOrderedQueryable<Job> getAllJobs()
        {
            return _context.jobs.AsNoTracking().OrderBy(p => p.Id);
            //return _context.Set<Job>().AsEnumerable();
        }

        public new int Update(Job entity)
        {
            _context.Attach(entity);
            _context.Entry(entity).Property(x => x.CompanyName).IsModified = true;
            _context.Entry(entity).Property(x => x.JobTitle).IsModified = true;
            //_context.Entry(entity).Property(x => x.BusinessContent).IsModified = true;
            _context.Entry(entity).Property(x => x.BusinessStreamID).IsModified = true;
            _context.Entry(entity).Property(x => x.ContractType_Text).IsModified = true;
            _context.Entry(entity).Property(x => x.JapaneseLevel_Text).IsModified = true;
            _context.Entry(entity).Property(x => x.Role).IsModified = true;
            _context.Entry(entity).Property(x => x.Salary_Hourly).IsModified = true;
            _context.Entry(entity).Property(x => x.Salary_Monthly).IsModified = true;
            _context.Entry(entity).Property(x => x.Transporationfee).IsModified = true;
            _context.Entry(entity).Property(x => x.Workinghour).IsModified = true;
            _context.Entry(entity).Property(x => x.WorkingdaysPerweek).IsModified = true;
            _context.Entry(entity).Property(x => x.WorkLocationAddress).IsModified = true;
            _context.Entry(entity).Property(x => x.Email).IsModified = true;
            _context.Entry(entity).Property(x => x.NeededStaff).IsModified = true;
            _context.Entry(entity).Property(x => x.Status).IsModified = true;

            return _context.SaveChanges();
        }

        public IEnumerable<Job> getJobbyLocation(Func<Job, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public IOrderedQueryable<Job> GetJobsByProvince(string province)
        {

            return _context.jobs.Where(x => x.provinceName == province && x.Status == true).OrderBy(p => p.Id);
        }

        public IOrderedQueryable<Job> GetSearchedJob(string provinceName, string searchString)
        {
            return _context.jobs.Where(x => (x.JobTitle.Contains(searchString) ||(x.CompanyName.Contains(searchString)))  && x.provinceName.Contains(provinceName) && x.Status == true).OrderBy(p => p.Id);
            //return _context.jobs.Where(x => (x.JobTitle.Contains(searchString) || (x.CompanyName.Contains(searchString))) && x.provinceName.Contains(provinceName)).OrderBy(p => p.Id);
        }

        public Job GetJobsById(long jobId)
        {
            return _context.Find<Job>(jobId);
        }

        public IEnumerable<Job> getJobWithType(Func<Job, bool> predicate)
        {
            throw new NotImplementedException();
        }

    }
}
