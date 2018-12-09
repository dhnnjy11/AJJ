using System;
using System.Collections.Generic;
using System.Text;

namespace Ajj.Core.Interface
{
    public interface IMarketoLead
    {
        IInput[] input { get; set; }
    }

    public class IInput
    {
        string firstName { get; set; }
        string lastName { get; set; }
        string email { get; set; }
        string title { get; set; }
        string gender { get; set; }
    }
}
