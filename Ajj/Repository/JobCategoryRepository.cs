using Ajj.Data;
using Ajj.Interface;
using Ajj.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Ajj.Repository
{
    public class JobCategoryRepository : Repository<JobCategory>, IJobCategoryRepository
    {
        public JobCategoryRepository(ApplicationDbContext context) : base(context)
        {
        }

        

    }
}
