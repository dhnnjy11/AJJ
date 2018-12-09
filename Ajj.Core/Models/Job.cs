using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Ajj.Models
{
    [Table("ajjjobs")]
    public class Job
    {
        [Key]
        public long Id { get; set; }
        public string BusinessContent { get; set; }
        public string CompanyName { get; set; }
        public string JobTitle { get; set; }
        public int CompanyId { get; set; }
        public string ContactDepartment { get; set; }
        public string ContactPerson { get; set; }
        public long Job_type_id { get; set; }
        public string Email { get; set; }
        public string Frequencyofwork { get; set; }
        public string HPURL { get; set; }
        public string HQAddress { get; set; }
        public string HQFax { get; set; }
        public string HQTel { get; set; }
        public string NeededStaff { get; set; }
        public string RepresentativeName { get; set; }
        public string Role { get; set; }
        public string Salary { get; set; }
        public string Transporationfee { get; set; }
        public string WorkingdaysPerweek { get; set; }
        public string Workinghour { get; set; }
        public string WorkinghourPerday { get; set; }
        public string ContractType { get; set; }
        public string WorkLocationAddress { get; set; }
        public string provinceName { get; set; }
        public string Category { get; set; }
        public string JapaneseLevel { get; set; }
        public string OtherRequirement { get; set; }
        public int BusinessStreamID { get; set; }
        public BusinessStream BusinessStream { get; set; }
        public bool Status { get; set; }
        public string UniqueId { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; }
        public DateTime PostDate { get; set; }
        public List<JobApply> JobsApply { get; set; }
    }
}
