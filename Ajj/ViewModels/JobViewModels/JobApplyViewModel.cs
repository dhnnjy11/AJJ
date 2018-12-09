using Ajj.Core.Interface;

namespace Ajj.Models.JobViewModels
{
    public class JobApplyViewModel
    {
        private readonly IBusinessStreamRepository _businessStreamRepository = null;

        public long JobID { get; set; }
        public int JobSeekerId { get; set; }
        public string IsExperience { get; set; }
        public string ExpYear { get; set; }
        public string ExpMonth { get; set; }

        public int MaxJobAllowed
        {
            get
            {
                return 3;
            }
        }
    }
}