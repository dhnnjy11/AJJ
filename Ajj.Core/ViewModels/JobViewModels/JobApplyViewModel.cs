using Ajj.Interface;
using System;

namespace Ajj.Models.JobViewModels
{
    public class JobApplyViewModel 
    {
        private readonly IBusinessStreamRepository _businessStreamRepository = null;

       

       
       

        public long JobID { get; set; }      
        public string IsExperience { get; set; }
        public string ExpYear { get; set; }
        public string ExpMonth { get; set; }
       

    }
}
