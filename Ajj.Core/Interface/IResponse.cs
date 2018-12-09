using Ajj.Core.Entities;
using System.Collections.Generic;

namespace Ajj.Core.Interface
{
    public interface IResponse
    {
        string Result { get; set; }
        IEnumerable<GBUser> UserInfo { get; set; }
        string Error { get; set; }
        string Hash { get; set; }
    }
}