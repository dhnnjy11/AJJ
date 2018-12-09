using Ajj.Areas.Clients.Models;
using Ajj.Core.Entities;
using Ajj.Core.Interface;
using Ajj.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MTIC.Service.Email;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Ajj.Areas.Clients.Controllers
{
    [Area("Clients")]
    [Route("clients")]
    [Authorize(Roles = "client,admin")]
    public class JobController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailService _emailService;
        private readonly IBusinessStreamRepository _businessRepository;
        private readonly IJobRepository _jobsRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IPostalCodeRepository _postalCodeRepository;
        private readonly IProvinceRepository _provinceRepository;
        private readonly IJobApplyRepository _jobApplyRepository;
        private readonly IJobSeekerRepository _jobseekerRepository;
        private readonly IJobCategoryRepository _jobCategoryRepository;
        private readonly IAutoFillService _autoFillService;
        private readonly IImportService _importService;

        public JobController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            IEmailService emailService,
            IBusinessStreamRepository businessRepository,
            IJobRepository jobsRepository,
            IClientRepository clientRepository,
            IPostalCodeRepository postalCodeRepository,
            IProvinceRepository provinceRepository,
            IJobApplyRepository jobApplyRepository,
            IJobSeekerRepository jobseekerRepository,
            IJobCategoryRepository jobCategoryRepository,
            IAutoFillService autoFillService,
            IImportService importService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _emailService = emailService;
            _businessRepository = businessRepository;
            _jobsRepository = jobsRepository;
            _clientRepository = clientRepository;
            _postalCodeRepository = postalCodeRepository;
            _provinceRepository = provinceRepository;
            _jobApplyRepository = jobApplyRepository;
            _jobseekerRepository = jobseekerRepository;
            _jobCategoryRepository = jobCategoryRepository;
            _autoFillService = autoFillService;
            _importService = importService;
        }

        [AllowAnonymous]
        [Route("/clients")]
        public async Task<IActionResult> IndexAsync()
        {
            List<ClientJobViewModel> model = new List<ClientJobViewModel>();

            if (!_signInManager.IsSignedIn(User))
            {
                return Redirect("~/Account/LoginClient");
            }
            else
            {
                System.Security.Claims.ClaimsPrincipal currentUser = User;
                var isClient = currentUser.IsInRole("client") || currentUser.IsInRole("client-editor");

                if (isClient)
                {
                    var user = await _userManager.GetUserAsync(User);
                    var client = _clientRepository.GetClientByUserId(user.Id);

                    IEnumerable<Job> jobs = _jobsRepository.Find(x => x.ClientId == client.Id);

                    foreach (var job in jobs)
                    {
                        ClientJobViewModel clientjobmodel = new ClientJobViewModel();
                        var jobcategory = _jobCategoryRepository.GetById(job.JobCategoryId);
                        clientjobmodel.JobId = job.Id;
                        clientjobmodel.JobTitle = jobcategory.CategoryName_JP;
                        clientjobmodel.JapaneseLevel = job.JapaneseLevel_Text;
                        clientjobmodel.Workinghour = job.Workinghour;
                        clientjobmodel.WorkingDaysPerWeek = job.WorkingdaysPerweek;
                        //clientjobmodel.ContractType = job.WorkinghourPerday;
                        clientjobmodel.Address = job.WorkLocationAddress;
                        clientjobmodel.ProvinceName = job.provinceName;
                        clientjobmodel.Salary = job.Salary_Hourly;
                        clientjobmodel.PostDate = job.PostDate;
                        clientjobmodel.Status = job.Status;
                        var jobapplied = _jobApplyRepository.GetAppliedCount(job.Id);
                        clientjobmodel.CandidateApplied = jobapplied.Count;
                        model.Add(clientjobmodel);
                    }

                    return View(model);
                }
            }

            return Redirect("~/Account/LoginClient");
        }

        [Route("Job/Add")]
        [HttpGet]
        public IActionResult AddJob()
        {
            ClientJobViewModel model = new ClientJobViewModel();
            string userId = _userManager.GetUserId(HttpContext.User);
            var clientInfo = _clientRepository.GetClientByUserId(userId);


            model.ClientID = clientInfo.Id;

            if (model.businessstreams.Count == 0)
            {
                model.businessstreams = new List<SelectListItem>();
                model.businessstreams.Add(new SelectListItem
                {
                    Value = "",
                    Text = "-- 業界選択 --"
                });

                var businessstreams = _businessRepository.GetAll();

                foreach (var BusinessStrem in businessstreams)
                {
                    model.businessstreams.Add(new SelectListItem
                    {
                        Value = Convert.ToString(BusinessStrem.Id),
                        Text = BusinessStrem.Name_jp
                    });
                }
            }
            
            if (model.Provinces.Count == 0)
            {
                model.Provinces = new List<SelectListItem>();
                model.Provinces.Add(new SelectListItem
                {
                    Value = "",
                    Text = "--Choose--"
                });

                var provinces = _provinceRepository.GetAll();

                foreach (var province in provinces)
                {
                    model.Provinces.Add(new SelectListItem
                    {
                        Value = Convert.ToString(province.Id),
                        Text = province.Name
                    });
                }
            }

            return View(model);
        }

        [Route("Job/Add")]
        [HttpPost]
        public IActionResult AddJob(ClientJobViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.businessstreams.Count == 0)
                {
                    model.businessstreams = new List<SelectListItem>();
                    model.businessstreams.Add(new SelectListItem
                    {
                        Value = "",
                        Text = "-- 業界選択 --"
                    });

                    var businessstreams = _businessRepository.GetAll();

                    foreach (var BusinessStrem in businessstreams)
                    {
                        model.businessstreams.Add(new SelectListItem
                        {
                            Value = Convert.ToString(BusinessStrem.Id),
                            Text = BusinessStrem.Name_jp
                        });
                    }
                }
                var jobcategory = _jobCategoryRepository.GetById(Convert.ToInt32(model.JobCategoryId));
                var job = new Job();

                job.JobCategory = jobcategory;
                job.JobTitle = jobcategory.CategoryName;
                job.ContractType_Text = model.ContractType;
                job.Salary_Hourly = model.Salary_Hourly;
                job.Salary_Monthly = model.Salary_Monthly;
                job.WorkingdaysPerweek = model.WorkingDaysPerWeek;
                job.Workinghour = model.Workinghour;
                job.Role = model.JobTitle; //here job title and role is same
                job.JapaneseLevel_Text = model.JapaneseLevel;
                job.Transporationfee = model.TransportationFee;
                job.WorkLocationAddress = model.Address;
                job.Client = _clientRepository.GetById(model.ClientID);
                job.CompanyName = job.Client.CompanyName;
                job.BusinessStream = _businessRepository.GetById(model.BusinessstreamID);
                job.NeededStaff = model.NeededStaff;
                job.provinceName = model.ProvinceName;
                //string workingTime = $"{model.WorkStartTime}-{model.WorkEndTime}";
                job.Status = model.Status;
                job.StartWorkingTime = model.WorkStartTime;
                job.EndWorkingTime = model.WorkEndTime;
                //string requiredAge = $"{model.MinAge}-{model.MaxAge}";
                //job.RequiredAge = requiredAge;
                job.MinAge = model.MinAge;
                job.MaxAge = model.MaxAge;
                job.PostDate = DateTime.Now;
                string postalCodeValue = model.PostalAddrss1 + "-" + model.PostalAddrss2;
                var postalCode = _postalCodeRepository.Find(x => x.Code == postalCodeValue).FirstOrDefault();
                job.PostalCode = postalCode;
                job.WorkLocationAddress = model.Address;
                job.ContractType = new ContractType();
                job.JapaneseLevel = new JapaneseLevel();

                int result = _jobsRepository.Create(job);
                if (result > 0)
                {
                    return Redirect("~/Clients");
                }
                else
                {
                    ViewData["error"] = "Error in saving data";
                    return View(model);
                }
            }
            else
            {
                ViewBag.ErrorMsg = "Validation Failed";
                return View(model);
            }
        }

        [Route("Job/Edit/{id:int}")]
        [HttpGet]
        public async Task<IActionResult> EditJob(long id)
        {
            ClientJobViewModel model = new ClientJobViewModel();
            model.businessstreams = new List<SelectListItem>();
            model.businessstreams.Add(new SelectListItem
            {
                Value = "",
                Text = "-- 業界選択 --"
            });

            var businessstreams = _businessRepository.GetAll();

            foreach (var BusinessStrem in businessstreams)
            {
                model.businessstreams.Add(new SelectListItem
                {
                    Value = Convert.ToString(BusinessStrem.Id),
                    Text = BusinessStrem.Name_jp
                });
            }

            if (model.Provinces.Count == 0)
            {
                model.Provinces = new List<SelectListItem>();
                model.Provinces.Add(new SelectListItem
                {
                    Value = "",
                    Text = "--Choose--"
                });

                var provinces = _provinceRepository.GetAll();

                foreach (var prov in provinces)
                {
                    model.Provinces.Add(new SelectListItem
                    {
                        Value = Convert.ToString(prov.Id),
                        Text = prov.Name_Jp
                    });
                }
            }

            Job job = _jobsRepository.GetJobsById(id);
            var jobCategories = await _jobCategoryRepository.FindByAsyn(x => x.BusinessStreamId == job.BusinessStreamID);

            var postalCode = _postalCodeRepository.GetById(job.PostalCodeId);
            var province = _provinceRepository.GetById(postalCode.ProvinceID);

            List<SelectListItem> jobCategoryList = new List<SelectListItem>();
            foreach (var jobcategory in jobCategories)
            {
                jobCategoryList.Add(new SelectListItem()
                {
                    Value = Convert.ToString(jobcategory.Id),
                    Text = jobcategory.CategoryName_JP,
                });
            }
            var postalArry = postalCode.Code.Split('-');
            if (postalArry.Length > 0)
            {
                model.PostalAddrss1 = postalArry[0];
                model.PostalAddrss2 = postalArry[1];
            }
            model.ProvinceID = province.Id;
            model.CityName = postalCode.CityName;
            model.Town = postalCode.Town;
            model.JobCategories = jobCategoryList;
            model.JobId = job.Id;
            model.Workinghour = job.Workinghour;
            model.WorkingDaysPerWeek = job.WorkingdaysPerweek;
            model.JapaneseLevel = job.JapaneseLevel_Text;
            model.BusinessstreamID = job.BusinessStreamID;
            model.ClientID = job.ClientId;
            model.ContractType = job.ContractType_Text;
            model.Salary_Monthly = job.Salary_Monthly;
            model.Salary_Hourly = job.Salary_Hourly;
            model.NeededStaff = job.NeededStaff;
            model.TransportationFee = job.Transporationfee;
            model.Address = job.WorkLocationAddress;
            model.ProvinceName = job.provinceName;
            model.JobCategoryId = job.JobTitle;
            model.WorkStartTime = job.StartWorkingTime;
            model.WorkEndTime = job.EndWorkingTime;
            model.Status = job.Status;
            //if (!string.IsNullOrEmpty(job.WorkingTime))
            //{
            //    string []workingtime = job.WorkingTime.Split("-");
            //    if(workingtime.Length > 0)
            //    {
            //        model.WorkStartTime = workingtime[0];
            //        model.WorkEndTime = workingtime[1];
            //    }
            //}
            model.MinAge = job.MinAge;
            model.MaxAge = job.MaxAge;

            //if (!string.IsNullOrEmpty(job.RequiredAge))
            //{
            //    string[] requiredages = job.RequiredAge.Split("-");
            //    if(requiredages.Length > 0)
            //    {
            //        model.MinAge = Convert.ToInt32(requiredages[0]);
            //        model.MaxAge = Convert.ToInt32(requiredages[1]);
            //    }
            //}
            return View(model);
        }

        [Route("Job/Edit")]
        [HttpPost]
        public IActionResult EditJob(ClientJobViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.businessstreams = new List<SelectListItem>();
                model.businessstreams.Add(new SelectListItem
                {
                    Value = "",
                    Text = "-- 業界選択 --"
                });

                var businessstreams = _businessRepository.GetAll();

                foreach (var BusinessStrem in businessstreams)
                {
                    model.businessstreams.Add(new SelectListItem
                    {
                        Value = Convert.ToString(BusinessStrem.Id),
                        Text = BusinessStrem.Name_jp
                    });
                }

                var jobcategory = _jobCategoryRepository.GetById(Convert.ToInt32(model.JobCategoryId));
                var job = _jobsRepository.GetJobsById(model.JobId);

                job.JobCategory = jobcategory;
                job.JobTitle = jobcategory.CategoryName;
                job.ContractType_Text = model.ContractType;
                job.Salary_Hourly = model.Salary_Hourly;
                job.Salary_Monthly = model.Salary_Monthly;
                job.WorkingdaysPerweek = model.WorkingDaysPerWeek;
                job.Workinghour = model.Workinghour;
                job.Role = model.JobTitle; //here job title and role is same
                job.JapaneseLevel_Text = model.JapaneseLevel;
                job.Transporationfee = model.TransportationFee;
                job.WorkLocationAddress = model.Address;
                job.Client = _clientRepository.GetById(model.ClientID);
                job.CompanyName = job.Client.CompanyName;
                job.BusinessStream = _businessRepository.GetById(model.BusinessstreamID);
                job.NeededStaff = model.NeededStaff;
                job.provinceName = model.ProvinceName;
                //string workingTime = $"{model.WorkStartTime}-{model.WorkEndTime}";
                job.StartWorkingTime = model.WorkStartTime;
                job.EndWorkingTime = model.WorkEndTime;
                job.MinAge = model.MinAge;
                job.MaxAge = model.MaxAge;
                //string requiredAge = $"{model.MinAge}-{model.MaxAge}";
                //job.RequiredAge = requiredAge;
                job.Status = model.Status;
                job.PostDate = DateTime.Now;

                int result = _jobsRepository.Update(job);
                if (result > 0)
                {
                    if (User.IsInRole("admin"))
                    {
                        //ViewData["success"] = "Job updated successfully";
                        return Redirect("/admin/jobinfo/index");
                        //return View(job.Id);
                    }

                    return Redirect("~/clients");

                }
                else
                {
                    ViewData["error"] = "Error in editing data";
                    return View(model);
                }
            }
            else
            {
                ViewData["error"] = "Validation Failed";
                return View(model);
            }
        }

        [Route("Job/Delete/{id:int}")]
        [HttpPost]
        public IActionResult DeleteJob(int id)
        {
            Job job = new Job()
            {
                Id = id
            };
            try
            {
                _jobsRepository.Delete(job);
            }
            catch (Exception ex)
            {
                ViewData["error"] = "Error in Deleting Job";
            }

            return Redirect("~/Clients");
        }

        [Route("Job/Detail/{id:long}")]
        [HttpGet]
        public async Task<IActionResult> JobDetail(long id)
        {
            var jobdetail = await _jobsRepository.GetJobsByIdAsync(id);

            var model = new ClientJobViewModel()
            {
                JobTitle = jobdetail.JobCategory.CategoryName_JP,
                BusinessstreamName = jobdetail.BusinessStream.Name_jp,
                JobId = jobdetail.Id,
                JapaneseLevel = jobdetail.JapaneseLevel_Text,
                ContractType = jobdetail.ContractType_Text,
                businessstream = jobdetail.BusinessStream,
                Workinghour = jobdetail.Workinghour,
                WorkingDaysPerWeek = jobdetail.WorkingdaysPerweek,
                NeededStaff = jobdetail.NeededStaff,
                Salary_Monthly = jobdetail.Salary_Monthly,
                Salary_Hourly = jobdetail.Salary_Hourly
            };

            return View(model);
        }

        [HttpPost]
        [Route("Job/PublishJob/{id:long}")]
        public async Task<bool> PublishJobAsync(long id)
        {
            Job job = _jobsRepository.GetJobsById(id);
            job.Status = true;
            var updatestatus = _jobsRepository.Update(job);
            if (updatestatus == 1)
            {
                return true;
            }
            return false;
        }

        [HttpPost]
        [Route("Job/UnPublishJob/{id:long}")]
        public async Task<bool> UnPublishJobAsync(long id)
        {
            Job job = _jobsRepository.GetJobsById(id);
            job.Status = false;
            var updatestatus = _jobsRepository.Update(job);
            if (updatestatus == 1)
            {
                return true;
            }
            return false;
        }

        [HttpGet]
        [Route("Job/Candidates/{id:long}")]
        public async Task<IActionResult> CandidatesAsync(long id)
        {
            List<CandidateViewModel> model = new List<CandidateViewModel>();
            IEnumerable<JobApply> jobapplies = _jobApplyRepository.Find(x => x.JobID == id);

            foreach (var jobapply in jobapplies)
            {
                string address1 = "";
                ApplicationUser appuser = await _userManager.FindByIdAsync(jobapply.UserID);
                var jobseeker = _jobseekerRepository.FindByAsyn(x => x.ApplicationUserId == appuser.Id).Result.FirstOrDefault();
                CandidateViewModel candidate = new CandidateViewModel();
                var postalCodeDetail = _postalCodeRepository.GetPostalCodeDetail(jobseeker.PostalAddrss);
                if (postalCodeDetail != null)
                {
                    var province = _provinceRepository.GetById(postalCodeDetail.ProvinceID);
                    if (province != null)
                    {
                        address1 = postalCodeDetail.Code + " " + province.Name_Jp + " " + postalCodeDetail.CityName + " " + postalCodeDetail.CityName;
                    }
                }

                candidate.Name = jobseeker.LastName + " " + jobseeker.FirstName;
                candidate.EmailAddress = appuser.Email;
                candidate.AppliedDate = jobapply.ApplyDate;
                candidate.Address = address1 + " " + jobseeker.Address;
                candidate.ContactNumber = appuser.PhoneNumber;
                model.Add(candidate);
            }

            return View(model);
        }

        //[Route("Job/ImportCSV")]
        //public IActionResult ImportCSV()
        //{
        //    MTIC.Service.Import.ImportJob importjob = new MTIC.Service.Import.ImportJob();
        //    importjob.ImportCSV(@"JobData\CompanyJobFormat.csv");
        //    return View();
        //}

        [Route("Job/JobSubCategory")]
        public async Task<IEnumerable<JobCategory>> JobSubCategory(int id)
        {
            var model = await _jobCategoryRepository.FindByAsyn(x => x.BusinessStreamId == id);
            return model.ToList();
        }

        //[HttpPost]
        //[Route("Upload")]
        //[Obsolete]
        //public async Task<IActionResult> UploadAsync(List<IFormFile> files)
        //{
        //    IList<Job> jobList = new List<Job>();
        //    try
        //    {
        //        long size = files.Sum(f => f.Length);
        //        if (size > 0)
        //        {
        //            foreach (var formFile in files)
        //            {
        //                Stream fileStream = formFile.OpenReadStream();
        //                var clientData = await _importService.ImportExcelAsync(fileStream);

        //                foreach (DataRow dr in clientData.Rows)
        //                {
        //                    Job model = new Job();
        //                    if (dr["Client_Id"] == null)
        //                    {
        //                        throw new Exception("Client value can't null");
        //                    }
        //                    var client = _clientRepository.GetById(Convert.ToInt32(dr["Client_Id"] ?? 0));
        //                    model.Client = client;
        //                    model.CompanyName = client.CompanyName;
        //                    model.ContractType_Text = Convert.ToString(dr["Contract_Type"]);
        //                    model.NeededStaff = Convert.ToString(dr["Needed_Staff"]);
        //                    model.Role = Convert.ToString(dr["Role"]);
        //                    model.Salary_Monthly = Convert.ToString(dr["Salary_Monthly"]);
        //                    model.Salary_Hourly = Convert.ToString(dr["Salary_Hourly"]);
        //                    if (dr["Is_Trasporation_Include"] == null || dr["Is_Trasporation_Include"].ToString().Trim() == "")
        //                    {
        //                        throw new Exception("Transporation_Include value can't null");
        //                    }
        //                    model.TrasportationIncluded = Convert.ToChar(dr["Is_Trasporation_Include"]);
        //                    model.Transporationfee = Convert.ToString(dr["Transporation_Fee_Max"]);
        //                    model.WorkingdaysPerweek = Convert.ToString(dr["Working_Days_Per_Week"]);
        //                    model.Workinghour = Convert.ToString(dr["Working_Hours_Per_Day"]);

        //                    if (dr["Postal_Code"] == null || dr["Postal_Code"].ToString().Trim() == "")
        //                    {
        //                        throw new Exception("Postal ID value can't be null");
        //                    }
        //                    var postalCode = _postalCodeRepository.GetPostalCodeDetail(Convert.ToString(dr["Postal_Code"] ?? 0));
        //                    model.PostalCode = postalCode;
        //                    model.provinceName = Convert.ToString(dr["Prefrecture"]);
        //                    if (dr["Job_Category_Id"] == null || dr["Job_Category_Id"].ToString().Trim() == "")
        //                    {
        //                        throw new Exception("Job Category can't be null");
        //                    }
        //                    var jobCategory = _jobCategoryRepository.GetById(Convert.ToInt32(dr["Job_Category_Id"]));

        //                    model.JobCategory = jobCategory;
        //                    model.JobTitle = jobCategory.CategoryName;
        //                    if (dr["Status"] == null || dr["Status"].ToString().Trim() == "")
        //                    {
        //                        throw new Exception("Job Category can't be null");
        //                    }
        //                    model.Status = Convert.ToBoolean(dr["Status"].ToString() == "1" ? 1 : 0);
        //                    model.BusinessStreamID = Convert.ToInt32(dr["Business_Stream_Id"]);
        //                    model.JapaneseLevel_Text = Convert.ToString(dr["Japanese_Level"]);
        //                    //model.JobTitle_JP = Convert.ToString(dr["Job_Title_JP"]);
        //                    model.WorkLocationAddress = Convert.ToString(dr["Job_Address"]);
        //                    model.PostDate = DateTime.Now;
        //                    model.JapaneseLevel = new JapaneseLevel();
        //                    model.ContractType = new ContractType();
        //                    jobList.Add(model);
        //                }
        //                await _jobRepository.AddListAsyn(jobList);
        //                await _jobRepository.SaveAsync();
        //                //return Ok(new { message = "OK" });
        //                TempData["success"] = "Successfully Uploaded";
        //            }
        //        }
        //        else
        //        {
        //            TempData["error"] = "No record found";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        TempData["error"] = "Error in uploading data";
        //        //return BadRequest(new { error = ex.Message });
        //    }
        //    return RedirectToAction("IndexAsync");
        //    //return View(jobList);
        //}

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
                    var user = await _userManager.GetUserAsync(HttpContext.User);
                    var client = _clientRepository.GetClientByUserId(user.Id);

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
                            //int clientsId = Convert.ToInt32(dr["Client_Id"]);
                            //var client = _clientRepository.GetById(Convert.ToInt32(dr["Client_Id"] ?? 0));
                            model.Client = client ?? throw new Exception($"client do not exists");
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