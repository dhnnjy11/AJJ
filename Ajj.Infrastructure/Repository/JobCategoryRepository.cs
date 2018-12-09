using Ajj.Core.Entities;
using Ajj.Core.Interface;

using Ajj.Infrastructure.Data;

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Ajj.Infrastructure.Repository
{
    public class JobCategoryRepository : Repository<JobCategory>, IJobCategoryRepository
    {
        public JobCategoryRepository(ApplicationDbContext context) : base(context)
        {
        }

        

    }
}
