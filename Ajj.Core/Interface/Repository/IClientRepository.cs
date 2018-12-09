using Ajj.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ajj.Core.Interface
{
    public interface IClientRepository : IRepository<Client>
    {
        Client GetClientByUserId(string applicationId);

        CompanyImage GetComapnyImage(int id);

        Task<IEnumerable<CompanyUser>> GetClientMapData(int clientId);
    }
}