using Ajj.Core.Interface;

namespace Ajj.Core.Entities.MarketoAPI
{
    public class MarketoLead : IMarketoLead
    {
        public IInput[] input { get; set; }
    }

    public class Input : IInput
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string title { get; set; }
        public string gender { get; set; }
    }
}