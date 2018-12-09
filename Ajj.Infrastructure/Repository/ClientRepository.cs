using Ajj.Core.Entities;
using Ajj.Core.Interface;
using Ajj.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ajj.Infrastructure.Repository
{
    public class ClientRepository : Repository<Client>, IClientRepository
    {
        public ClientRepository(ApplicationDbContext context) : base(context)
        {
        }

        public Client GetClientByUserId(string applicationId)
        {
            return _context.companyusers
                           .Include(x => x.Client)
                           .Include(x => x.ApplicationUser)
                           .Where(x => x.ApplicationUserId == applicationId)
                           .Select(x => x.Client)
                           .FirstOrDefault();
        }

        public CompanyImage GetComapnyImage(int id)
        {
            return _context.companyimages.Where(x => x.ClientId == id)
                .OrderByDescending(x => x.Id).FirstOrDefault();
        }

        public async Task<IEnumerable<CompanyUser>> GetClientMapData(int clientId)
        {
            return await _context.companyusers.Where(x=> x.ClientId == clientId).ToListAsync();
        }
    }
}