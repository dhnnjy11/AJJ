using Ajj.Core.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ajj.Areas.Clients.Models
{
    public class ClientJobViewModel
    {
        public long JobId { get; set; }
        public string JobTitle { get; set; }

        public string JobDescription { get; set; }
        public string Workinghour { get; set; }
        public string WorkingDaysPerWeek { get; set; }
        public string ContractType { get; set; }
        public string JapaneseLevel { get; set; }
        public string ProvinceName { get; set; }
        public string Role { get; set; }
        public bool Status { get; set; } //published or unpublished
        public string Salary { get; set; }
        public string Salary_Hourly { get; set; }
        public string Salary_Monthly { get; set; }
        public string WorkStartTime { get; set; }
        public string WorkEndTime { get; set; }
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
        public int ClientID { get; set; }
        public string CompanyName { get; set; }
        public string NeededStaff { get; set; }
        public string PostalAddrss1 { get; set; }
        public string PostalAddrss2 { get; set; }
        public int ProvinceID { get; set; }
        public List<SelectListItem> Provinces { get; set; } = new List<SelectListItem>();
        [Required(ErrorMessage = "Postal Code is not valid")]
        public string CityName { get; set; }
        public string Town { get; set; }
        public string Address { get; set; }
        public char TrasportationIncluded { get; set; }
        public string TransportationFee { get; set; }
        public int CandidateApplied { get; set; }
        public DateTime PostDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string BusinessstreamName { get; set; }
        public int BusinessstreamID { get; set; }
        public BusinessStream businessstream { get; set; }
        public List<SelectListItem> businessstreams { get; set; } = new List<SelectListItem>();
        public string JobCategoryId { get; set; }
        public List<SelectListItem> JobCategories { get; set; } = new List<SelectListItem>();
    }
}