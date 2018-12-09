using Ajj.Core.Entities;
using Ajj.Core.Interface;
using Ajj.Infrastructure.Data;

namespace Ajj.Infrastructure.Repository
{
    public class ProvinceRepository : Repository<Province>, IProvinceRepository
    {
        public ProvinceRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}