using Ajj.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ajj.Core.Interface
{

    public interface IJobRepository : IRepository<Job>
    {
        IOrderedQueryable<Job> getAllJobs();
        IEnumerable<Job> getJobWithType(Func<Job, bool> predicate);
        IEnumerable<Job> getJobbyLocation(Func<Job, bool> predicate);
        IOrderedQueryable<Job> GetJobsByProvince(string province);
        Job GetJobsById(long jobId);
        Task<Job> GetJobsByIdAsync(long jobId);
        IOrderedQueryable<Job> GetSearchedJob(string provinceName, string searchString);
        //IQueryable<Job> FindJobs(string provinceName,string category, string[] searchString);
        IQueryable<Job> FindJobs(string provinceName,string jobCategory, string[] searchString);
        IQueryable<Job> SearchKeywords();
        IEnumerable<Job> GetJobsDynamic(string query);
    }
}
