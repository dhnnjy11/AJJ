using Ajj.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ajj.Core.Interface
{
    public interface IJobSeekerRepository : IRepository<JobSeeker>
    {
        JobSeeker GetJobSeekerByUserId(string applicationId);
    }
}
