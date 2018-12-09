using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ajj.Core.Entities
{
    [Table("ajjjobs")]
    public class Job
    {
        [Key]
        public long Id { get; set; }

        public string CompanyName { get; set; }
        public string JobTitle { get; set; } //subcategory
        public string JobTitle_JP { get; set; } //subcategory
        public string ContactPerson { get; set; }
        public string Email { get; set; }
        public string NeededStaff { get; set; }
        //public string RepresentativeName { get; set; }
        public string Role { get; set; }
        public string Salary_Hourly { get; set; }
        public string Salary_Monthly { get; set; }
        public char TrasportationIncluded { get; set; }
        public string Transporationfee { get; set; }
        public string WorkingdaysPerweek { get; set; }
        public string Workinghour { get; set; }
        public string ContractType_Text { get; set; }
        public string WorkLocationAddress { get; set; }
        public string provinceName { get; set; }
        public string JapaneseLevel_Text { get; set; }
        public string OtherRequirement { get; set; }
        public string WorkingTime { get; set; }
        public string StartWorkingTime { get; set; }
        public string EndWorkingTime { get; set; }
        [MaxLength(100)]
        public string RequiredAge { get; set; }
        public int MinAge { get; set; } = 0;
        public int MaxAge { get; set; } = 100;
        public char? GenderRequired { get; set; } 
        public int BusinessStreamID { get; set; } //main category
        public BusinessStream BusinessStream { get; set; }

        public int JobCategoryId { get; set; }
        public JobCategory JobCategory { get; set; }
        public bool Status { get; set; }

        public int PostalCodeId { get; set; }
        public PostalCode PostalCode { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; }
        public int ContractTypeId { get; set; }
        public ContractType ContractType { get; set; }
        public int JapaneseLevelId { get; set; }
        public JapaneseLevel JapaneseLevel { get; set; }
        public DateTime PostDate { get; set; }
        public List<JobApply> JobsApply { get; set; }
    }
}