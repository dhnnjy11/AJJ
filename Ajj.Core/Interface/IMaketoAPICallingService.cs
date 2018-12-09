using Ajj.Core.Entities.MarketoAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ajj.Core.Interface
{
    public interface IMaketoAPICallingService
    {

        MarketoResponse CreateUpdateLead(IMarketoLead lead);
    }
}
