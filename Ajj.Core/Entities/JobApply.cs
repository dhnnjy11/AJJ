﻿using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ajj.Core.Entities
{
    [Table("jobapplies")]
    public class JobApply
    {
        //[Key]
        //public int id { get; set; }      
        public long JobID { get; set; }
        public Job Job { get; set; }
        public int? JobSeekerId { get; set; }
        public JobSeeker JobSeeker { get; set; }
        public string UserID { get; set; }
        public ApplicationUser User { get; set; }
        public string JobTitle { get; set; }
        public bool IsExperience { get; set; }
        public string Experience { get; set; }        
        public DateTime ApplyDate { get; set; }

    }
}
