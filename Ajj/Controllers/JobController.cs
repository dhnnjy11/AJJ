using Ajj.Core.Entities;
using Ajj.Core.Interface;
using Ajj.Core.Interface.Repository;
using Ajj.Extensions;
using Ajj.Models.JobViewModels;
using Ajj.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ReflectionIT.Mvc.Paging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Ajj.Controllers
{
    public class JobController : Controller
    {
        private readonly IJobRepository _IJobsRepository;
        private readonly IJobApplyRepository _IJobsApplyRepository;
        private readonly IRepository<VisaCategory> _visaCategoryRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailService _emailService;
        private readonly IClientRepository _clientRepository;
        private readonly IBusinessStreamRepository _businessStreamRepository;
        private readonly IPostalCodeRepository _postalCodeRepository;
        private readonly IJobSeekerRepository _jobSeekerRepository;
        private readonly IRepository<CompanyImage> _companyImageRepository;
        private readonly IJobSkillRepository _jobSkillRepository;

        public JobController(IJobRepository IJobsRepository,
            IJobApplyRepository IJobsApplyRepository,
            IRepository<VisaCategory> visaCategoryRepository,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailService emailService,
            IClientRepository clientRepository,
            IBusinessStreamRepository businessStreamRepository,
            IPostalCodeRepository postalCodeRepository,
            IJobSeekerRepository jobSeekerRepository,
            IRepository<CompanyImage> companyImageRepository,
            IJobSkillRepository jobSkillRepository)
        {
            _IJobsRepository = IJobsRepository;
            _IJobsApplyRepository = IJobsApplyRepository;
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
            _clientRepository = clientRepository;
            _businessStreamRepository = businessStreamRepository;
            _postalCodeRepository = postalCodeRepository;
            _jobSeekerRepository = jobSeekerRepository;
            _companyImageRepository = companyImageRepository;
            _jobSkillRepository = jobSkillRepository;
        }

        //public async Task<IActionResult> Chiba(int page = 1)
        //{
        //    var jobs = _IJobsRepository.GetJobsByProvince("chiba");
        //    var model = await PagingList.CreateAsync<Job>(jobs, 10, page, "Chiba");
        //    ViewData["TotalJobsFound"] = model.TotalRecordCount;
        //    return View(model);
        //}

        //public async Task<IActionResult> Tokyo(int page = 1)
        //{
        //    var jobs = _IJobsRepository.GetJobsByProvince("tokyo");
        //    var model = await PagingList.CreateAsync<Job>(jobs, 10, page, "tokyo");
        //    ViewData["TotalJobsFound"] = model.TotalRecordCount;
        //    return View(model);
        //}

        //public async Task<IActionResult> Gunma(int page = 1)
        //{
        //    var jobs = _IJobsRepository.GetJobsByProvince("gunma");
        //    var model = await PagingList.CreateAsync<Job>(jobs, 10, page, "Gunma");
        //    ViewData["TotalJobsFound"] = model.TotalRecordCount;
        //    return View(model);
        //}

        //public async Task<IActionResult> Ibaraki(int page = 1)
        //{
        //    var jobs = _IJobsRepository.GetJobsByProvince("ibaraki");
        //    var model = await PagingList.CreateAsync<Job>(jobs, 10, page, "Ibaraki");
        //    ViewData["TotalJobsFound"] = model.TotalRecordCount;
        //    return View(model);
        //}

        //public async Task<IActionResult> Kanagawa(int page = 1)
        //{
        //    var jobs = _IJobsRepository.GetJobsByProvince("kanagawa");
        //    var model = await PagingList.CreateAsync<Job>(jobs, 10, page, "Kanagawa");
        //    ViewData["TotalJobsFound"] = model.TotalRecordCount;
        //    return View(model);
        //}

        //public async Task<IActionResult> Saitma(int page = 1)
        //{
        //    var jobs = _IJobsRepository.GetJobsByProvince("saitma");
        //    var model = await PagingList.CreateAsync<Job>(jobs, 10, page, "Saitma");
        //    ViewData["TotalJobsFound"] = model.TotalRecordCount;
        //    return View(model);
        //}

        //public async Task<IActionResult> Tochigi(int page = 1)
        //{
        //    var jobs = _IJobsRepository.GetJobsByProvince("tochigi");
        //    var model = await PagingList.CreateAsync<Job>(jobs, 10, page, "Tochigi");
        //    ViewData["TotalJobsFound"] = model.TotalRecordCount;
        //    return View(model);
        //}

        //[Authorize(Roles = "candidate")]

        public IActionResult JobDetails(long id, string returnUrl = null)
        {
            Job job = _IJobsRepository.GetJobsById(id);
            Client client = _clientRepository.GetById(job.ClientId);
            var businessStream = _businessStreamRepository.GetById(job.BusinessStreamID);
            var postalcode = new PostalCode();
            if (job.PostalCodeId != 0)
            {
                postalcode = _postalCodeRepository.GetById(job.PostalCodeId);
            }
            JobDetailsViewModel model = new JobDetailsViewModel(job, client, businessStream, postalcode);

            return View(model);
        }

        public IActionResult List(string ProvinceName)
        {
            //JobDetailsViewModel model = new JobDetailsViewModel { User = user, Job = job };
            return View();
        }

        [HttpPost, Route("Job/JobDetails")]
        [Authorize(Roles = "candidate")]
        public async Task<IActionResult> JobDetailsAsync(JobApplyViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await _userManager.GetUserAsync(User);
                    var jobseeker = _jobSeekerRepository.GetJobSeekerByUserId(user.Id);
                    var jobcount = _IJobsApplyRepository.JobAppliedCount(user);
                    if (jobcount >= model.MaxJobAllowed)
                    {
                        TempData["error"] = "You have already applied for max allowed jobs for today, Please try again tommorow";
                        return View("JobApplyResult");
                    }

                    if (_IJobsApplyRepository.IsAlreadyApplied(user.Id, model.JobID))
                    {
                        RedirectToAction("AlreadyApplied");
                    }

                    JobApply jobapply = new JobApply();

                    jobapply.JobID = model.JobID;
                    jobapply.UserID = user.Id;
                    jobapply.JobSeeker = jobseeker;
                    jobapply.ApplyDate = DateTime.Now;
                    if (model.IsExperience != null)
                    {
                        jobapply.IsExperience = Convert.ToBoolean(model.IsExperience);
                    }
                    if (!String.IsNullOrEmpty(model.ExpYear) && !String.IsNullOrEmpty(model.ExpMonth))
                    {
                        jobapply.Experience = Convert.ToString(model.ExpYear + " years " + model.ExpMonth + " months ");
                    }

                    _IJobsApplyRepository.Create(jobapply);
                    List<EmailAddress> emailAddress = new List<EmailAddress>()
                    {
                        new EmailAddress()
                        {
                            Name = user.UserName,
                            Address = user.Email
                        }
                        //,
                        //new EmailAddress()
                        //{
                        //    Name = "AJJ Job Apply Alert",
                        //    Address = "info@gaijinbank.com"
                        //},
                    };

                    var job = _IJobsRepository.GetJobsById(jobapply.JobID);
                    var jobSeeker = _jobSeekerRepository.Find(x => x.ApplicationUserId == user.Id).FirstOrDefault();
                    string jobtitle = job.JobTitle;
                    string companyName = job.CompanyName;
                    string candidateName = jobSeeker.LastName + " " + jobSeeker.FirstName;
                    TempData["warning"] = $"You can apply total {model.MaxJobAllowed} jobs in one day, Today your have already applied for {jobcount + 1} job";
                    _emailService.SendEmailAsync2(emailAddress, "jobapply", jobtitle, companyName, candidateName);
                }

                return RedirectToAction("JobSavedSuccess");
            }
            //catch (Microsoft.EntityFrameworkCore.DbUpdateException ex)
            //{
            //    return RedirectToAction("AlreadyApplied");
            //}
            catch (Exception ex)
            {
                ViewData["error"] = "Some Error while apply for job";
            }
            return View("JobDetails/" + model.JobID);
        }

        public IActionResult AlreadyApplied()
        {
            return View();
        }

        public IActionResult JobSavedSuccess()
        {
            return View();
        }

        public IActionResult JobApplyResult()
        {
            return View();
        }

        public IEnumerable<string> GetJobCategoies()
        {
            var result = _IJobsRepository.getAllJobs().Select(x => x.BusinessStream.Name).Distinct().ToList();
            return result.ToArray();
        }

        [Route("Job/SearchJobs")]
        public async Task<IActionResult> SearchJobsAsync(string provinceName = "", string jobCategory = "", string searchString = "", int page = 1)
        {
            if (provinceName == null)
            {
                provinceName = "";
            }
            if (jobCategory == null)
            {
                jobCategory = "";
            }
            if (searchString == null)
            {
                searchString = "";
            }
            TempData["province"] = provinceName;
            TempData["searchstring"] = searchString;
            TempData["jobcategory"] = jobCategory;
            var searchkeywords = searchString.Trim().TrimEnd(',').Split(',');
            var jobs = _IJobsRepository.FindJobs(provinceName.ToLower(), jobCategory.Trim(), searchkeywords)
                .OrderByDescending(x => Guid.NewGuid());
            var viewModel = jobs.Select(job => new JobDetailsViewModel()
            {
                JobID = job.Id,
                JobTitle = job.JobTitle,
                PostDate = job.PostDate.ToString("yyyy-M-dd"),
                CompanyName = job.Client.CompanyName,
                WorkingHours = job.Workinghour,
                Salary_Hourly = job.Salary_Hourly,
                Salary_Monthly = job.Salary_Monthly,
                ProvinceName = job.PostalCode.Province.Name,
                ContractType = job.ContractType_Text,
                Town = job.PostalCode.Town,
                CompanyImageUrl = job.Client.CompanyImages.Count == 0 ? job.BusinessStream.CategoryImageUrl : job.Client.CompanyImages.OrderByDescending(x => x.Id).FirstOrDefault().ImagePath,
                //CompanyImageUrl = _clientRepository.GetComapnyImage(job.Client.Id).ImagePath ?? job.BusinessStream.CategoryImageUrl,
                Town_En = job.PostalCode.Town_En,
                CityName_En = job.PostalCode.CityName_En
            }).ToList().AsEnumerable().OrderByDescending(x => Guid.NewGuid());

            var model = PagingList.Create(viewModel, 10, page, "SearchJobsAsync");
            model.ProvinceName = provinceName;
            model.SearchString = searchString;
            model.JobCategory = jobCategory;
            ViewData["TotalJobsFound"] = model.TotalRecordCount;
            return View(model);
        }

        [Authorize(Roles = "candidate")]
        public IActionResult SelectPrefrences(int param = 0)
        {
            var user = _userManager.GetUserAsync(HttpContext.User).Result;
            var jobseeker = _jobSeekerRepository.GetJobSeekerByUserId(user.Id);
            if (param == 0)
            {
                if (jobseeker.InitialLoginCount > 1)
                {
                    return RedirectToAction("Index", "Home");
                }

                jobseeker.InitialLoginCount += 1;
                jobseeker.UpdatedDate = DateTime.Now;
                _jobSeekerRepository.Update(jobseeker);
            }
            var jobSkills = jobseeker.JobSkills.Select(x => x.BusinessStreamId);

            var businessStreams = _businessStreamRepository.GetAll()
                .Where(x => x.Name != null & x.Name != "")
                .Select(x => new PrefrencesViewModel()
                {
                    HasJob = x.HasJob,
                    Id = x.Id,
                    IsSelected = jobSkills.Contains(x.Id),
                    Name = x.Name
                });

            return View(businessStreams);
        }

        [Authorize(Roles = "candidate"), HttpPost]
        public async Task<IActionResult> SelectPrefrencesAsync(IFormCollection collection)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var jobseeker = _jobSeekerRepository.GetJobSeekerByUserId(user.Id);
            List<JobSkill> jobskillList = new List<JobSkill>();

            _jobSkillRepository.DeleteList(jobseeker.JobSkills);

            foreach (var id in collection["item.IsSelected"])
            {
                
                int.TryParse(id, out int jobskillId);
                if (jobskillId != 0)
                {
                    var jobskill = new JobSkill();
                    jobskill.BusinessStream = _businessStreamRepository.GetById(jobskillId);
                    //jobskill.JobCategory = new JobCategory();

                    jobskill.JobSeeker = jobseeker;
                    jobskillList.Add(jobskill);
                }
            }

            _jobSkillRepository.AddList(jobskillList);
            await _jobSkillRepository.SaveAsync();
            jobseeker.InitialLoginCount = 2;
            jobseeker.UpdatedDate = DateTime.Now;
            _jobSeekerRepository.Update(jobseeker);
            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "admin")]
        public IActionResult SaveIntoDB()
        {
            //JArray o1 = JArray.Parse(System.IO.File.ReadAllText(@"JobData\tokyo.json"));
            string[] dirs = Directory.GetFiles(@"JobData", "*.json");
            foreach (var file in dirs)
            {
                List<Job> jobs =
                    JsonConvert.DeserializeObject<List<Job>>(System.IO.File.ReadAllText(file));
                foreach (var job in jobs)
                {
                    job.PostDate = DateTime.Now.Date;
                    _IJobsRepository.Create(job);
                }
            }

            return View();
        }
    }
}