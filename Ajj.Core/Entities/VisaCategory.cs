using System;
using System.Collections.Generic;
using System.Text;

namespace Ajj.Core.Entities
{
    public class VisaCategory : BaseEntity<int>
    {
        public int ParentId { get; set; }
        public string Name { get; set; } // Main visa Category
        public string SubCategory { get; set; } // Visa sub category
        public bool IsActive { get; set; } // Y = Active for applying jobs, N = Not active for applying jobs
        public char AllowJobStatus { get; set; } //A = All Jobs, P = Partial Jobs, T = Time Bounded (Part-time), N = Not Allowed To Job
        public char NeedPermission { get; set; } //N = Not need of Permission, Y = Need Permssion to Work
        public readonly List<VisaJobMap> _jobmaps = new List<VisaJobMap>();


    }
}
