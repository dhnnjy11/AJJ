using Ajj.Core.Entities;
using Ajj.Core.Interface.Repository;
using Ajj.Infrastructure.Data;

namespace Ajj.Infrastructure.Repository
{
    public class JobSkillRepository : Repository<JobSkill>, IJobSkillRepository
    {
        public JobSkillRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}