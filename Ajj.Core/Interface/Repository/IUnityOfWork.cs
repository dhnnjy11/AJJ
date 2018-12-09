using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ajj.Core.Interface
{
    public interface IUnityOfWork
    {
        IClientRepository ClientRepository { get; }
        IBusinessStreamRepository BusinessStreamRepository { get; }
        IPostalCodeRepository PostalcodeRepository { get; }
        void Save();
    }
}
