using Ajj.Core.Entities;
using Ajj.Core.Interface;
using System;

using System.Linq;


namespace Ajj.Core.Services
{
    public class JobSeekerServices : IJobSeekerService
    {
        private readonly IJobRepository _jobsRepository;
        private readonly IJobSeekerRepository _jobSeekerRepository;
        private readonly IBusinessStreamRepository _businessStreamRepository;
        private readonly IRepository<VisaCategory> _visaCategory;

        public JobSeekerServices(IJobRepository jobsRepository,
            IJobSeekerRepository jobSeekerRepository,
            IBusinessStreamRepository businessStreamRepository,
            IRepository<VisaCategory> visaCategory)
        {
            _jobsRepository = jobsRepository;
            _jobSeekerRepository = jobSeekerRepository;
            _businessStreamRepository = businessStreamRepository;
            _visaCategory = visaCategory;
        }
       
        public VisaCategory GetVisa(string category, string subCategory)
        {
            try
            {
                
                category = category ?? "";
                subCategory = subCategory ?? "";
              
                var visa = _visaCategory.GetAll().Where(x => x.Name == category.Trim() && x.SubCategory==subCategory.Trim()).FirstOrDefault();
                return visa;
            }
            catch(Exception ex)
            {

            }
            return null;
           

        }

        public void RecommendedJobs()
        {
            
        }

    }
}
