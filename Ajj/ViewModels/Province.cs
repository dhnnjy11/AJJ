using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ajj.Models
{
    public class Province
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Name_Jp { get; set; }
        public bool HasJob { get; set; }
        public bool IsActive { get; set; }
        public List<JobSeeker> Jobseekers { get; set; }
        //public List<Province> Provinces { get; set; }
        //public List<Country> Countries { get; set; }
    }
}
