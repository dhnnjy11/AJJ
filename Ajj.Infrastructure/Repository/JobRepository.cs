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
    public class JobRepository : Repository<Job>, IJobRepository
    {
        public JobRepository(ApplicationDbContext context) : base(context)
        {
        }

        public IOrderedQueryable<Job> getAllJobs()
        {
            return _context.jobs.Where(x=>x.Status == true).AsNoTracking().OrderBy(p => p.Id);
            //return _context.Set<Job>().AsEnumerable();

        }

        //public new int Update(Job entity)
        //{
       //     _context.Attach(entity);
       //     _context.Entry(entity).Property(x => x.CompanyName).IsModified = true;
       //     _context.Entry(entity).Property(x => x.JobTitle).IsModified = true;
       //     //_context.Entry(entity).Property(x => x.BusinessContent).IsModified = true;
       //     _context.Entry(entity).Property(x => x.BusinessStreamID).IsModified = true;
       //     _context.Entry(entity).Property(x => x.ContractType_Text).IsModified = true;
       //     _context.Entry(entity).Property(x => x.JapaneseLevel_Text).IsModified = true;
       //     _context.Entry(entity).Property(x => x.Role).IsModified = true;
       //     _context.Entry(entity).Property(x => x.Salary_Hourly).IsModified = true;
       //     _context.Entry(entity).Property(x => x.Salary_Monthly).IsModified = true;
       //     _context.Entry(entity).Property(x => x.Workinghour).IsModified = true;
       //     _context.Entry(entity).Property(x => x.WorkingdaysPerweek).IsModified = true;
       //     _context.Entry(entity).Property(x => x.WorkLocationAddress).IsModified = true;
       //     _context.Entry(entity).Property(x => x.Email).IsModified = true;
       //     _context.Entry(entity).Property(x => x.NeededStaff).IsModified = true;
       //     _context.Entry(entity).Property(x => x.Status).IsModified = true;

        //    return _context.SaveChanges();
        //}

        public IEnumerable<Job> getJobbyLocation(Func<Job, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public IOrderedQueryable<Job> GetJobsByProvince(string province)
        {
            return _context.jobs.Where(x => x.provinceName == province && x.Status == true).OrderBy(p => p.Id);

        }

        public IQueryable<Job> SearchKeywords()
        {
            var searchedJobs = _context.jobs
                .Include(business => business.BusinessStream)
                .Include(cat => cat.JobCategory)
                .Include(client => client.Client)
                .Include(postalcode => postalcode.PostalCode)
                    .ThenInclude(provice => provice.Province)
                .Where(x => x.Status == true);

            return searchedJobs;
        }

        public IQueryable<Job> FindJobs(string provinceName,string jobCategory, string[] searchKeywords)
        {
            var searchedjobs = _context.jobs
                .Include(business => business.BusinessStream)
                .Include(cat => cat.JobCategory)
                .Include(client => client.Client)
                    .ThenInclude(img=>img.CompanyImages)
                .Include(postalcode => postalcode.PostalCode)
                    .ThenInclude(provice => provice.Province)
                .Where(x => x.Status == true);

            IQueryable<Job> searchResult = Enumerable.Empty<Job>().AsQueryable();
            foreach (var keyword in searchKeywords)
            {
                var skeyword = keyword.Trim().ToLower();
                if (searchResult.Count() == 0)
                {
                    searchResult = searchedjobs.Where(x => (x.Client.CompanyName.ToLower().Contains(skeyword) || (x.JobCategory.CategoryName.ToLower().Contains(skeyword))|| x.PostalCode.CityName_En.ToLower().Contains(skeyword)) && x.BusinessStream.Name.ToLower().Contains(jobCategory) && x.PostalCode.Province.Name.Contains(provinceName));
                }
                else
                {
                    var secondSearchResult = searchedjobs.Where(x => (x.Client.CompanyName.ToLower().Contains(skeyword) || (x.JobCategory.CategoryName.ToLower().Contains(skeyword)) || x.PostalCode.CityName_En.ToLower().Contains(skeyword)) && x.BusinessStream.Name.ToLower().Contains(jobCategory) && x.PostalCode.Province.Name.Contains(provinceName));

                    searchResult = searchResult.Union(secondSearchResult);
                }

            };

            return searchResult;
        }

        public IOrderedQueryable<Job> GetSearchedJob(string provinceName, string searchString)
        {
            var searchedjobs = _context.jobs
                .Include(cat => cat.JobCategory)
                .Include(client => client.Client)
                .Include(postalcode => postalcode.PostalCode)
                    .ThenInclude(provice => provice.Province);

            return _context.jobs.Where(x => (x.JobTitle.Contains(searchString) || (x.CompanyName.Contains(searchString))) && x.provinceName.Contains(provinceName)).OrderByDescending(x => x.Id);
        }

        public Job GetJobsById(long jobId)
        {
            return _context.Find<Job>(jobId);
        }

        public async Task<Job> GetJobsByIdAsync(long jobId)
        {
            return await _context.jobs.Where(x => x.Id == jobId).Include(cat => cat.JobCategory).Include(business => business.BusinessStream).SingleOrDefaultAsync();
        }

        public IEnumerable<Job> getJobWithType(Func<Job, bool> predicate)
        {
            throw new NotImplementedException();
        }
        
        public IEnumerable<Job> GetJobsByBusinessStream(BusinessStream businessstream,int age, string prefrecture)
        {
                           

            return _context.jobs.Include(x => x.BusinessStream)
                .Where(x=>x.BusinessStream == businessstream);
        }

        public IEnumerable<Job> GetJobsDynamic(string query)
        {
            return _context.jobs
                .Include(x=>x.BusinessStream)
                .Include(x=>x.PostalCode)
                .Where(x => x.Status == true)                
                .FromSql(query);
            
        }

       
    }
}