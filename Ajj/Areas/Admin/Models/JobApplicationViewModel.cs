using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ajj.Areas.Admin.Models
{
    public class JobApplicationViewModel
    {
        public int ApplicantId { get; set; }
        public string ApplicantEmail { get; set; }
        public string CompanyName { get; set; }
        public string JobTitle { get; set; }
        public string ApplyDate { get; set; }
        public long JobId { get; set; }
    }
}
