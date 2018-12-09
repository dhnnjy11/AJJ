using Ajj.Data;
using Ajj.Interface;
using Ajj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ajj.Repository
{
    public class CountryRepository : Repository<Country>,ICountryRepository
    {
        public CountryRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
