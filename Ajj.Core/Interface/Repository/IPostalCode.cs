using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ajj.Core.Entities;


namespace Ajj.Core.Interface
{
   public interface IPostalCodeRepository : IRepository<PostalCode>
    {
        PostalCode GetPostalCodeDetail(string postalcode);
    }
}
