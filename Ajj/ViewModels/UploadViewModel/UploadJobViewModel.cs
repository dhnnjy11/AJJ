using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ajj.ViewModels.UploadViewModel
{
    public class UploadJobViewModel
    {
        [Required(ErrorMessage = "Client ID has some missing value")]
        public string ClientId { get; set; }
        public string ContractType { get; set; }
        public string NeededStaff { get; set; }
        public string Role { get; set; }
        public string SalaryMonthly { get; set; }
        public string SalaryHourly { get; set; }
        [Required(ErrorMessage = "Is Transportation has some missing value")]
        public string IsTrasporationInclude { get; set; }
        public string TransporationFeeMax { get; set; }
        public string WorkingDaysPerWeek { get; set; }
        public string WorkingHoursPerDay { get; set; }
        public string PostalCodeId { get; set; }
        public string Prefrecture { get; set; }
        public string JobAddress { get; set; }
        [Required(ErrorMessage = "Status has some missing value")]
        public string Status { get; set; }
        public string JobCategoryId { get; set; }
        public string BusinessStreamId { get; set; }
        public string JapaneseLevel { get; set; }
        public string JobTitleJP { get; set; }


    }
}
