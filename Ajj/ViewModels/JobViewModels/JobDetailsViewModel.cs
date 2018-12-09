using Ajj.Core.Entities;
using Ajj.Core.Interface;
using Ajj.Infrastructure.Repository;
using System;
using System.Linq;

namespace Ajj.Models.JobViewModels
{
    public class JobDetailsViewModel
    {
        private readonly IBusinessStreamRepository _businessStreamRepository ;
        private readonly IRepository<CompanyImage> _companyImageRepository;
        

        public JobDetailsViewModel()
        {
       //     _companyImageRepository = companyImageRepository;
       //     CompanyImageUrl =  _companyImageRepository.Find(x => x.ClientId == clientid)
       //         .OrderByDescending(x => x.Id)
       //         .FirstOrDefault().ImagePath;
        }
        public JobDetailsViewModel(IBusinessStreamRepository businessRepository, IRepository<CompanyImage> companyImageRepository)
        {
            _businessStreamRepository = businessRepository;
            _companyImageRepository = companyImageRepository;
        }

        public string GetCompanyImage(Client client, BusinessStream businessStream)
        {
            var companyImage = _companyImageRepository.Find(x => x.ClientId == client.Id).FirstOrDefault();
            if (companyImage != null)
            {
                CompanyImageUrl = companyImage.ImagePath;
            }
            else
            {
                CompanyImageUrl = businessStream.CategoryImageUrl;
            }
            return CompanyImageUrl;
        }

        public JobDetailsViewModel(Job job, Client client, BusinessStream businessStream, PostalCode postalCode)
        {
            if (client != null && job != null)
            {
                JobID = job.Id;
                JobTitle = job.JobTitle.Trim().Replace("\n","");
                PostDate = job.PostDate.ToString("yyyy-M-dd");
                var days = (DateTime.Now - job.PostDate).TotalDays;             
                CompanyName = client.CompanyName ?? "";
                WorkingHours = job.Workinghour;
                Salary_Hourly = job.Salary_Hourly;
                Salary_Monthly = job.Salary_Monthly;
                ProvinceName = postalCode.Province.Name_Jp;
                JapaneseLevel = job.JapaneseLevel_Text;
                TransportationFee = job.Transporationfee;
                WebsiteUrl = client.WebsiteUrl;
                ContractType = job.ContractType_Text;
                CityName = postalCode.CityName;
                WorkingAddress = job.WorkLocationAddress;
                Town = postalCode.Town;
                WorkingTime = job.WorkingTime;
                //RequiredAge = job.RequiredAge;
                MinAge = job.MinAge;
                MaxAge = job.MaxAge;
                StartWorkTime = job.StartWorkingTime;
                EndWorkTime = job.EndWorkingTime;
                //var companyImage = _companyImageRepository.Find(x => x.ClientId == client.Id).FirstOrDefault();
                //if (companyImage != null)
                //{
                //    CompanyImageUrl = companyImage.ImagePath;
                //}
                //else
                //{
                //    CompanyImageUrl = businessStream.CategoryImageUrl;
                //}
                Town_En = postalCode.Town_En;
                CityName_En = postalCode.CityName_En;

                if (businessStream != null)
                {
                    IndustryName = businessStream.Name;
                }
            }
        }

        public long JobID { get; set; }
        public int JobSeekerId { get; set; }
        public string JobTitle { get; set; }
        public string IsExperience { get; set; }
        public string ExpYear { get; set; }
        public string ExpMonth { get; set; }
        public string CompanyEmail { get; set; }
        public string PostDate { get; set; }
        public string PostedDays
        {
            get
            {
                var daysPassed = (DateTime.Now - Convert.ToDateTime(PostDate)).TotalDays;
                return String.Format("{0:0}", daysPassed);
            }
        }
        public string CompanyName { get; set; }
        public string WorkingHours { get; set; }
        public string WorkingDays { get; set; }
        public string Salary_Hourly { get; set; }
        public string Salary_Monthly { get; set; }
        public string ProvinceName { get; set; }
        public string SearchString { get; set; }
        public string JapaneseLevel { get; set; }
        public string TransportationFee { get; set; }
        public string WebsiteUrl { get; set; }
        public string WorkingAddress { get; set; }
        public string ContractType { get; set; }
        public string IndustryName { get; set; }
        public string CityName { get; set; }
        public string Town { get; set; }
        public string CityName_En { get; set; }
        public string Town_En { get; set; }
        public string CompanyImageUrl { get; set; }
        public string WorkingTime { get; set; }
        public string StartWorkTime { get; set; }
        public string EndWorkTime { get; set; }
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
        public string GetPostDate()
        {
            var daysPassed = (DateTime.Now - Convert.ToDateTime(PostDate)).TotalDays;
            return String.Format("{0:0}", daysPassed);
        }
    }
}