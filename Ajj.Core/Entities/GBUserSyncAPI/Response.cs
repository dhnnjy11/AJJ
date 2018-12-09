using Ajj.Core.Interface;
using System.Collections.Generic;

namespace Ajj.Core.Entities.GBUserSyncAPI
{
    public class Response : IResponse
    {
        public string Result { get; set; }
        public IEnumerable<GBUser> UserInfo { get; set; }
        public string Error { get; set; }
        public string Hash { get; set; }
    }
}