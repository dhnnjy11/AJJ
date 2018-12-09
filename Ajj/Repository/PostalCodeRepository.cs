using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ajj.Data;
using Ajj.Interface;
using Ajj.Models;

namespace Ajj.Repository
{
    public class PostalCodeRepository : Repository<PostalCode>, IPostalCodeRepository
    {
        public PostalCodeRepository(ApplicationDbContext context) : base(context)
        {
          
        }

        public PostalCode GetPostalCodeDetail(string postalcode)
        {
            return _context.postalcodes.SingleOrDefault(x => x.Code == postalcode);
            //return _context.Set<Job>().AsEnumerable();
        }
    }
}
