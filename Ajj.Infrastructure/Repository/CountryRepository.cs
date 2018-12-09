
using Ajj.Core.Entities;
using Ajj.Core.Interface;
using Ajj.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ajj.Infrastructure.Repository
{
    public class CountryRepository : Repository<Country>,ICountryRepository
    {
        public CountryRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
