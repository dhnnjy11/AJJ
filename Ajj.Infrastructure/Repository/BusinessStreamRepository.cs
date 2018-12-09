using Ajj.Core.Entities;
using Ajj.Core.Interface;
using Ajj.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ajj.Infrastructure.Repository
{
    public class BusinessStreamRepository : Repository<BusinessStream>, IBusinessStreamRepository
    {
        public BusinessStreamRepository(ApplicationDbContext context) : base(context)
        {
        }

        public Task<List<BusinessStream>> GetAllowedCategoryAsync(int visaCategoryId, char allowStatus)
        {
            if(allowStatus == 'A' || allowStatus == 'T') //A = All jobs, T = Time bounded jobs like part-time but all jobs
            {
                return _context.businessstream                        
                        .ToListAsync();
            }
            return _context.visajobmap
                .Include(x => x.BusinessStream)
                .Include(x => x.VisaCategory)
                .Where(x =>x.VisaCategoryId == visaCategoryId)
                .Select(x=>x.BusinessStream)
                .ToListAsync();
               
        }
    }
}