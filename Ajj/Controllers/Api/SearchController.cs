using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ajj.Core.Interface;

using Microsoft.AspNetCore.Mvc;


namespace Ajj.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/Search")]
    public class SearchController : Controller
    {

        private readonly IJobRepository _jobsRepository;
        private readonly IJobApplyRepository _jobsApplyRepository;
        private readonly IJobCategoryRepository _jobCategoryRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IBusinessStreamRepository _businessStreamRepository;

        public SearchController(IJobRepository IJobsRepository,
            IJobApplyRepository IJobsApplyRepository,
            IJobCategoryRepository jobCategoryRepository,
            IClientRepository clientRepository,
            IBusinessStreamRepository businessStreamRepository)
        {
            _jobsRepository = IJobsRepository;
            _jobsApplyRepository = IJobsApplyRepository;
            _jobCategoryRepository = jobCategoryRepository;
            _clientRepository = clientRepository;
            _businessStreamRepository = businessStreamRepository;

        }
        // GET: api/Search
        [HttpGet(Name = "GetSeachKeyword")]
        public IEnumerable<string> GetAsync(string term = "")
        {
            IEnumerable<string> searchkeywords = new List<string>();
            if (!String.IsNullOrEmpty(term))
            {
                var result = _jobsRepository.SearchKeywords();

                var cities = result.Where(x=>x.PostalCode.CityName_En.Contains(term)).Select(x=>x.PostalCode.CityName_En.ToLower()).Distinct();
                //var businessCat = result
                //                .Where(x => x.BusinessStream.Name
                //                .Contains(term)).Select(x => x.BusinessStream.Name).Distinct();
                var companies = result.Where(x => x.Client.CompanyName
                                .Contains(term)).Select(x => x.Client.CompanyName).Distinct();
                var jobcategories = result
                                .Where(x => x.JobCategory.CategoryName.Contains(term))
                                .Select(x => x.JobCategory.CategoryName).Distinct();

                jobcategories = jobcategories.Concat(cities).Concat(companies);

                searchkeywords = jobcategories.ToArray();
            }


            return searchkeywords;
        }

       


    }
}
