using Ajj.Core.Entities;
using Ajj.Core.Interface;
using Ajj.Models.JobViewModels;
using Ajj.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Linq.Dynamic.Core;

using System.Threading.Tasks;

namespace Ajj.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin")]
    [Authorize(Roles = "admin")]
    public class JobInfoController : Controller
    {
        private readonly IImportService _importService;
        private readonly IClientRepository _clientRepository;
        private readonly IJobRepository _jobsRepository;
        private readonly IJobApplyRepository _IJobsApplyRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IJobCategoryRepository _jobCategoryRepository;
        private readonly IBusinessStreamRepository _businessStreamRepository;
        private readonly IPostalCodeRepository _postalCodeRepository;
        private readonly IJobSeekerRepository _jobSeekerRepository;

        public JobInfoController(IImportService importService,
            IClientRepository clientRepository,
            IJobRepository jobsRepository,
              IJobApplyRepository IJobsApplyRepository,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IJobCategoryRepository jobCategoryRepository,
            IBusinessStreamRepository businessStreamRepository,
            IPostalCodeRepository postalCodeRepository,
            IJobSeekerRepository jobSeekerRepository)
        {
            _importService = importService;
            _jobsRepository = jobsRepository;
            _IJobsApplyRepository = IJobsApplyRepository;
            _userManager = userManager;
            _signInManager = signInManager;
            _jobCategoryRepository = jobCategoryRepository;
            _clientRepository = clientRepository;
            _businessStreamRepository = businessStreamRepository;
            _postalCodeRepository = postalCodeRepository;
            _jobSeekerRepository = jobSeekerRepository;
        }

        [Route("JobInfo/Index")]
        public async Task<IActionResult> IndexAsync()
        {
            var jobs = await _jobsRepository.GetAllAsyn();

            List<JobDetailsViewModel> jobdetailList = new List<JobDetailsViewModel>();

            foreach (var job in jobs)
            {
                var client = _clientRepository.GetById(job.ClientId);
                var businessStream = _businessStreamRepository.GetById(job.BusinessStreamID);
                var postalcode = _postalCodeRepository.GetById(job.ClientId);
                //var postalcode = new PostalCode();
                //if (job.PostalCodeId != 0)
                //{
                //    postalcode = _postalCodeRepository.GetById(job.PostalCodeId);
                //}

                //var postalcode = _posta
                JobDetailsViewModel jobTempObj = new JobDetailsViewModel(job, client, businessStream, postalcode);
                jobdetailList.Add(jobTempObj);
            }

            IEnumerable<JobDetailsViewModel> list = jobdetailList.AsEnumerable();
           // return Ok(jobdetailList);
            return View(list);
        }

        [Route("JobInfo/AllJobs")]
        public async Task<IEnumerable<JobDetailsViewModel>> AllJobsAsync()
        {
            var jobs = await _jobsRepository.GetAllAsyn();

            List<JobDetailsViewModel> jobdetailList = new List<JobDetailsViewModel>();
            foreach (var job in jobs)
            {
                var client = _clientRepository.GetById(job.ClientId);
                var businessStream = _businessStreamRepository.GetById(job.BusinessStreamID);
                var postalcode = new PostalCode();
                if (job.PostalCodeId != 0)
                {
                    postalcode = _postalCodeRepository.GetById(job.PostalCodeId);
                }

                //var postalcode = _posta
                JobDetailsViewModel jobTempObj = new JobDetailsViewModel(job, client, businessStream, postalcode);
                jobdetailList.Add(jobTempObj);
            }

            IEnumerable<JobDetailsViewModel> list = jobdetailList.AsEnumerable();
            return list;
        }

        [Route("JobInfo/AllJobsNew")]
        [Authorize(Roles = "admin")]
        public async Task<dynamic> AllJobsAsyncNew(DataTableAjaxPostModel model)
        {
            // action inside a standard controller
            int filteredResultsCount;
            int totalResultsCount;

            var searchBy = (model.search != null) ? model.search.value : null;
            var take = model.length;
            var skip = model.start;

            string sortBy = "";
            bool sortDir = true;

            if (model.order != null)
            {
                // in this example we just default sort on the 1st column
                sortBy = model.columns[model.order[0].column].data;
                sortDir = model.order[0].dir.ToLower() == "asc";
            }

            if (String.IsNullOrEmpty(searchBy))
            {
                // if we have an empty search then just order the results by Id ascending
                sortBy = "Id";
                sortDir = true;
            }

            var jobs = await _jobsRepository.GetAllAsyn();
            totalResultsCount = jobs.Count();

            //var newObject = jobs.Select(x => new JobDetailsViewModel
            //{
            //    JobID = x.Id,
            //    JobTitle = x.JobTitle ?? "",
            //    CompanyName = x.CompanyName ?? "",
            //    PostDate = x.PostDate ,
            //    ProvinceName = x.provinceName ?? "",
            //    JapaneseLevel = x.JapaneseLevel_Text ?? ""

            //}).Skip(model.start).Take(model.length).ToList();

            IEnumerable<Job> searchedResult = null;

            if (!string.IsNullOrWhiteSpace(searchBy))
            {
                searchedResult = jobs.Where(x => x.Id.ToString().Contains(searchBy) || x.JobTitle.Contains(searchBy) || x.CompanyName.Contains(searchBy) || x.PostDate.ToString().Contains(searchBy) || x.provinceName.Contains(searchBy) || x.JapaneseLevel_Text.Contains(searchBy));
            }

            if (searchBy != null)
            {
                var b = searchedResult.Select(x => new string[]
                {
                    x.Id.ToString(),
                    x.JobTitle ?? "",
                    x.CompanyName ?? "",
                    x.provinceName ?? "",
                    x.PostDate.ToString(),
                });
            }

            var newObject = jobs.Select(x => new string[]
            {
                x.Id.ToString(),
                x.JobTitle ?? "",
                x.CompanyName ?? "",
                x.provinceName ?? "",
                x.PostDate.ToString(),
            });

            filteredResultsCount = newObject.Count();

            newObject = newObject.Skip(model.start)
            .Take(model.length);

            var secondObj = new
            {
                draw = model.draw,
                recordsTotal = totalResultsCount,
                recordsFiltered = filteredResultsCount,
                data = newObject
            };

            return secondObj;
        }

        [Route("JobInfo/AllJobsBak")]
        [HttpPost]
        public async Task<dynamic> AllJobsBakAsync(DataTableAjaxPostModel model)
        {
            // action inside a standard controller
            int filteredResultsCount;
            int totalResultsCount;

            var searchBy = (model.search != null) ? model.search.value : null;
            var take = model.length;
            var skip = model.start;

            string sortBy = "";
            string sortDir = "";

            if (model.order != null)
            {
                // in this example we just default sort on the 1st column
                sortBy = model.columns[model.order[0].column].data;
                sortDir = model.order[0].dir.ToLower();
            }

            var jobs = await _jobsRepository.GetAllAsyn();
            //convert to anonymous type object list as inummerable
            var newObject = jobs.Select(x => new
            {
                JobID = x.Id,
                JobTitle = _jobCategoryRepository.GetById(x.JobCategoryId).CategoryName_JP ?? "",
                CompanyName = _clientRepository.GetById(x.ClientId).CompanyName ?? "",
                ProvinceName = x.provinceName ?? "",
                x.PostDate
            });
            //searching
            totalResultsCount = newObject.Count();

            if (!string.IsNullOrWhiteSpace(searchBy))
            {
                searchBy = searchBy.ToLower();
                newObject = newObject.Where(x => x.JobID.ToString().Contains(searchBy) || x.JobTitle.ToLower().Contains(searchBy) || x.CompanyName.ToLower().Contains(searchBy) || x.PostDate.ToString().ToLower().Contains(searchBy) || x.ProvinceName.ToLower().Contains(searchBy));

                // if we have an empty search then just order the results by Id ascending
                sortBy = "jobID";
                sortDir = "asc";
            }
            filteredResultsCount = newObject.Count();
            newObject = newObject
                .AsQueryable()
                .OrderBy(sortBy + " " + sortDir)  //sorting
                .Skip(model.start)
                .Take(model.length)
                .ToList();

            var secondObj = new
            {
                model.draw,
                recordsTotal = totalResultsCount,
                recordsFiltered = filteredResultsCount,
                data = newObject
            };
            return secondObj;
        }

        [HttpPost]
        [Route("Upload")]        
        public async Task<IActionResult> UploadAsync(List<IFormFile> files)
        {
            IList<Job> jobList = new List<Job>();
            try
            {
                long size = files.Sum(f => f.Length);
                if (size > 0)
                {
                    foreach (var formFile in files)
                    {
                        Stream fileStream = formFile.OpenReadStream();
                        DataTable clientData = await _importService.ImportExcelAsync(fileStream);

                        //End of testing code 
                        foreach (DataRow dr in clientData.Rows)
                        {
                            Job model = new Job();
                            if (dr["Client_Id"] == null || dr["Client_Id"].ToString().Trim() == "")
                            {

                                throw new Exception("Client Id can't be null");
                            }
                            int clientsId = Convert.ToInt32(dr["Client_Id"]);
                            var client = _clientRepository.GetById(Convert.ToInt32(dr["Client_Id"] ?? 0));
                            model.Client = client ?? throw new Exception($"client {clientsId} do not exists");
                            model.CompanyName = client.CompanyName;
                            model.ContractType_Text = Convert.ToString(dr["Contract_Type"]);
                            model.NeededStaff = Convert.ToString(dr["Needed_Staff"]);
                            model.Role = Convert.ToString(dr["Role"]);
                            model.Salary_Monthly = Convert.ToString(dr["Salary_Monthly"]);
                            model.Salary_Hourly = Convert.ToString(dr["Salary_Hourly"]);
                            if (dr["Is_Trasporation_Include"] == null || dr["Is_Trasporation_Include"].ToString().Trim() == "")
                            {
                                throw new Exception("Transporation_Include value can't null");
                            }
                          
                            model.TrasportationIncluded = Convert.ToChar(dr["Is_Trasporation_Include"]);
                            model.Transporationfee = Convert.ToString(dr["Transporation_Fee_Max"]);
                            model.WorkingdaysPerweek = Convert.ToString(dr["Working_Days_Per_Week"]);
                            model.Workinghour = Convert.ToString(dr["Working_Hours_Per_Day"]);
                            if (dr["Postal_Code"] == null || dr["Postal_Code"].ToString().Trim() == "")
                            {
                                throw new Exception("Postal Code value can't be null");
                            }
                            string postalcodestr = Convert.ToString(dr["Postal_Code"]);
                            var postalCode = _postalCodeRepository.GetPostalCodeDetail(Convert.ToString(dr["Postal_Code"]));
                            model.PostalCode = postalCode ?? throw new Exception($"Postal Code {postalcodestr} do not exists");
                            model.provinceName = Convert.ToString(dr["Prefrecture"]);
                            if (dr["Job_Category_Id"] == null || dr["Job_Category_Id"].ToString().Trim() == "")
                            {
                                throw new Exception("Job Category can't be null");
                            }
                            int jobcategoryId = Convert.ToInt32(dr["Job_Category_Id"]);
                            var jobCategory = _jobCategoryRepository.GetById(Convert.ToInt32(dr["Job_Category_Id"]));

                            model.JobCategory = jobCategory ?? throw new Exception($"jobcategory {jobcategoryId} do not exists"); ;
                            model.JobTitle = jobCategory.CategoryName;
                            if (dr["Status"] == null || dr["Status"].ToString().Trim() == "")
                            {
                                throw new Exception("Status value should have 0 or 1");

                            }
                            model.Status = Convert.ToBoolean(dr["Status"].ToString() == "1" ? 1 : 0);
                            model.BusinessStreamID = Convert.ToInt32(dr["Business_Stream_Id"]);
                            model.JapaneseLevel_Text = Convert.ToString(dr["Japanese_Level"]);
                           // model.JobTitle_JP = Convert.ToString(dr["Job_Title_JP"]);
                            model.WorkLocationAddress = Convert.ToString(dr["Job_Address"]);
                            model.PostDate = DateTime.Now;
                            model.JapaneseLevel = new JapaneseLevel();
                            model.ContractType = new ContractType();
                            jobList.Add(model);
                        }
                        await _jobsRepository.AddListAsyn(jobList);
                        await _jobsRepository.SaveAsync();
                        TempData["success"] = "Successfully Uploaded";
                    }
                }
                else
                {
                    TempData["error"] = "Please browse excel file (format .xls,.xlsx) to upload and then click on upload button";
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }
            return RedirectToAction("IndexAsync");

        }
    }
}