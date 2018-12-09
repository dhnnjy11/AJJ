using Ajj.Core.Entities;
using Ajj.Core.Interface;
using Ajj.Data;
using Ajj.Interface;
using Ajj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ajj.Repository
{
    public class BusinessStreamRepository : Repository<BusinessStream>, IBusinessStreamRepository
    {
        public BusinessStreamRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
