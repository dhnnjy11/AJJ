using Ajj.Core.Entities;
using Ajj.Core.Interface;
using Ajj.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Ajj.Infrastructure.Repository
{
    public class PostalCodeRepository : Repository<PostalCode>, IPostalCodeRepository
    {
        public PostalCodeRepository(ApplicationDbContext context) : base(context)
        {
        }

        public new PostalCode GetById(int id)
        {
            return _context.postalcodes.Include(x => x.Province).SingleOrDefault(x=>x.Id == id);
        }
        public PostalCode GetPostalCodeDetail(string postalcode)
        {
            return _context.postalcodes.FirstOrDefault(x => x.Code == postalcode);
            //return _context.Set<Job>().AsEnumerable();
        }
    }
}