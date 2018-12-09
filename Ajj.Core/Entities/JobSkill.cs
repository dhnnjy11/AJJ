using System;
using System.Collections.Generic;
using System.Text;

namespace Ajj.Core.Entities
{
    public class JobSkill
    {
        
        public int Id { get; set; }
        public string SkillName { get; set; }
        public int BusinessStreamId { get; set; }
        public BusinessStream BusinessStream { get; set; } = new BusinessStream();
        //Not required now
        //public int JobCategoryId { get; set; }
        //public JobCategory JobCategory { get; set; } = new JobCategory();
        public int JobSeekerId { get; set; }
        public JobSeeker JobSeeker { get; set; } = new JobSeeker();


    }
}
