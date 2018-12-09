
using Ajj.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ajj.Core.Interface
{
    public interface IBusinessStreamRepository : IRepository<BusinessStream>
    {
        Task<List<BusinessStream>> GetAllowedCategoryAsync(int visaCategoryId, char allowStatus);
    }
}
