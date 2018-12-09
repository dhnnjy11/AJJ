using Ajj.Interface;
using System;

namespace Ajj.Models.JobViewModels
{
    public class JobDetailsViewModel 
    {
        private readonly IBusinessStreamRepository _businessStreamRepository = null;

        public JobDetailsViewModel(IBusinessStreamRepository businessRepository)
        {
            _businessStreamRepository = businessRepository;
        }

       
        public JobDetailsViewModel(Job job, Client client, BusinessStream businessStream)
        {
            JobID = job.Id;
            JobTitle = job.JobTitle;
            CompanyEmail = client.ContactEmail;
            var days = (DateTime.Now - job.PostDate).TotalDays;
            PostedDays = String.Format("{0:0}", days);
            CompanyName = client.CompanyName;
            WorkingHours = job.Workinghour;
            WorkingDays = job.WorkinghourPerday;
            Salary = job.Salary;
            ProvinceName = job.provinceName;
            JapaneseLevel = job.JapaneseLevel;
            TransportationFee = job.Transporationfee;
            WebsiteUrl = client.WebsiteUrl;
            ContractType = job.ContractType;
            IndustryName = businessStream.Name;
        }

        public long JobID { get; set; }
        public string JobTitle { get; set; }
        public string IsExperience { get; set; }
        public string ExpYear { get; set; }
        public string ExpMonth { get; set; }
        public string CompanyEmail { get; set; }
        public string PostedDays { get; set; }
        public string CompanyName { get; set; }
        public string WorkingHours { get; set; }
        public string WorkingDays { get; set; }
        public string Salary { get; set; }
        public string ProvinceName { get; set; }
        public string SearchString { get; set; }
        public string JapaneseLevel { get; set; }
        public string TransportationFee { get; set; }
        public string WebsiteUrl { get; set; }
        public string WorkingAddress { get; set; }
        public string ContractType { get; set; }
        public string IndustryName { get; set; }

    }
}
