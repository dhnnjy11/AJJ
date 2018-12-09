using Ajj.Core.Entities;
using Ajj.Core.Entities.MarketoAPI;
using Ajj.Core.Interface;
using Ajj.Extensions;
using Ajj.Models.HomeViewModel;
using Ajj.Service;
using Ajj.ViewModels.HomeViewModel;
using Ajj.ViewModels.UploadViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ajj.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmailSender _emailSender;
        private readonly IBusinessStreamRepository _businessRepository;
        private readonly IRepository<VisaCategory> _visaCategoryRepository;
        private readonly IProvinceRepository _provinceRepository;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IImportService _importService;
        private readonly IJobSeekerRepository _jobSeekerRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailService _emailService;
        private readonly IAPICallingService _aPICallingService;
        private readonly IJobRepository _jobRepository;
        private readonly IPostalCodeRepository _postalCodeRepository;

        private readonly IMaketoAPICallingService _maketoAPICallingService;

        public HomeController(IEmailSender emailSender, IBusinessStreamRepository businessRepository, IProvinceRepository provinceRepository,
            IRepository<VisaCategory> visaCategoryRepository,
            UserManager<ApplicationUser> userManager,
            IImportService importService, IHostingEnvironment hostingEnvironment,
            IJobSeekerRepository jobSeekerRepository,
            SignInManager<ApplicationUser> signInManager,
            IEmailService emailService,
            IAPICallingService aPICallingService,
            IJobRepository jobRepository,
            IPostalCodeRepository postalCodeRepository,
            IMaketoAPICallingService maketoAPICallingService
            )
        {
            _emailSender = emailSender;
            _userManager = userManager;
            _visaCategoryRepository = visaCategoryRepository;
            _businessRepository = businessRepository;
            _provinceRepository = provinceRepository;
            _importService = importService;
            _hostingEnvironment = hostingEnvironment;
            _jobSeekerRepository = jobSeekerRepository;
            _signInManager = signInManager;
            _emailService = emailService;
            _aPICallingService = aPICallingService;
            _jobRepository = jobRepository;
            _postalCodeRepository = postalCodeRepository;
            _maketoAPICallingService = maketoAPICallingService;
        }

        public IActionResult Index()
        {
            SearchJobViewModel model = new SearchJobViewModel();
            if (model.businessstreams.Count == 0)
            {
                model.businessstreams = new List<SelectListItem>();
                model.businessstreams.Add(new SelectListItem
                {
                    Value = "",
                    Text = "-- Choose  --"
                });

                var businessstreams = _businessRepository.GetAll();

                foreach (var BusinessStrem in businessstreams)
                {
                    model.businessstreams.Add(new SelectListItem
                    {
                        Value = Convert.ToString(BusinessStrem.Id),
                        Text = BusinessStrem.Name
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

            ViewData["HeaderSearch"] = true;
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        [HttpGet]
        public IActionResult Contact()
        {
            ViewData["Message"] = "Contact Us";

            return View();
        }

        [Route("Home/Contact")]
        [HttpPost]
        public async Task<IActionResult> ContactAsync(ContactViewModel model)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<b>Company Name</b> : " + model.CompanyName + "<br/>");
            sb.Append("<b>Person In Charge</b> : " + model.PersonIncharge + "<br/>");
            sb.Append("<b>Phone</b> : " + model.Phone + "<br/>");
            sb.Append("<b>Email Address</b> : " + model.EmailAddress + "<br/>");
            sb.Append("<b>Information Request</b> : " + model.InformationRequest + "<br/>");
            sb.Append("<b>Message</b> : " + model.Message + "<br/>");
            string body = sb.ToString();
            string subject = "AJJ Request Message";
            await _emailService.SendEmailAsync(model.EmailAddress, subject, body);
            ViewData["success"] = "Message Sent Successfully";
            return View("Contact");
        }

        [Route("Home/SendQuery")]
        [HttpGet]
        public IActionResult SendQueryAsync()
        {
            return View();
        }

        [Route("Home/SendQuery")]
        [HttpPost]
        public async Task<IActionResult> SendQueryAsync(SendQueryViewModel model)
        {
            if (ModelState.IsValid)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("<b>Name</b> : " + model.Name + "<br/>");
                sb.Append("<b>Email</b> : " + model.Email + "<br/>");
                sb.Append("<b>Subject</b> : " + model.Subject + "<br/>");
                sb.Append("<b>Description</b> : " + model.Description + "<br/>");
                string body = sb.ToString();
                string subject = "AJJ User Message";
                await _emailService.SendEmailAsync("dhananjay.singh@mtic.co.jp", subject, body);
                await _emailService.SendEmailAsync("registration-notice@jobsjapan.net", subject, body);
                ViewData["success"] = "Message Sent Successfully";
            }
            return View();
        }

        [HttpGet]
        public IActionResult GetSubVisa(int parentId)
        {
            var visaInfo = _visaCategoryRepository.Find(x => x.ParentId == parentId)
                .Where(x => x.IsActive == true)
                .Select(x =>
            new
            {
                x.Id,
                SubCategory = x.SubCategory.Trim(),
                x.NeedPermission
            })
            .Distinct();

            return Ok(visaInfo);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult CompanyInformation()
        {
            return View();
        }

        public async Task<IActionResult> TestAPI()
        {
            var jobseeker = await _jobSeekerRepository.GetAllAsyn();

            var jobseekerDetails = jobseeker.Select(x => new Input
            {
                email = x.ApplicationUser.Email,
                firstName = x.ApplicationUser.FirstName,
                lastName = x.ApplicationUser.LastName
            }).Take(300).ToArray();
            IMarketoLead lead = new MarketoLead();
            lead.input = jobseekerDetails;
            _maketoAPICallingService.CreateUpdateLead(lead);
            return Ok("success");
        }

        //public IActionResult ImportCSV()
        //{
        //    MTIC.Service.Import.ImportJob importjob = new MTIC.Service.Import.ImportJob();
        //    importjob.ImportCSV(@"JobData\CompanyJobFormat.csv");
        //    return View();
        //}

        public IActionResult TestUploadAsync()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> TestUploadAsync(List<IFormFile> files)
        {
            IList<UploadJobViewModel> modelList = new List<UploadJobViewModel>();
            //file2 = Request.Form.Files[0];
            long size = files.Sum(f => f.Length);
            if (size > 0)
            {
                foreach (var formFile in files)
                {
                    Stream fileStream = formFile.OpenReadStream();
                    //string rootFolder = _hostingEnvironment.WebRootPath;
                    //string fileName = @"jobs_list_upload_1001.xlsx";
                    //string file = Path.Combine(rootFolder, fileName);
                    //FileInfo fileInfo = new FileInfo(file);

                    var clientData = await _importService.ImportExcelAsync(fileStream);
                    foreach (DataRow dr in clientData.Rows)
                    {
                        UploadJobViewModel model = new UploadJobViewModel();
                        model.ClientId = Convert.ToString(dr["Client_Id"]);
                        model.ContractType = Convert.ToString(dr["Contract_Type"]);
                        model.NeededStaff = Convert.ToString(dr["Needed_Staff"]);
                        model.Role = Convert.ToString(dr["Role"]);
                        model.SalaryMonthly = Convert.ToString(dr["Salary_Monthly"]);
                        model.SalaryHourly = Convert.ToString(dr["Salary_Hourly"]);
                        model.IsTrasporationInclude = Convert.ToString(dr["Is_Trasporation_Include"]);
                        model.TransporationFeeMax = Convert.ToString(dr["Transporation_Fee_Max"]);
                        model.WorkingDaysPerWeek = Convert.ToString(dr["Working_Days_Per_Week"]);
                        model.WorkingHoursPerDay = Convert.ToString(dr["Working_Hours_Per_Day"]);
                        model.PostalCodeId = Convert.ToString(dr["Postal_Code_Id"]);
                        model.Prefrecture = Convert.ToString(dr["Prefrecture"]);
                        model.JobCategoryId = Convert.ToString(dr["JobCategoryId"]);
                        model.Status = Convert.ToString(dr["Status"]);
                        model.BusinessStreamId = Convert.ToString(dr["Business_Stream_Id"]);
                        model.JapaneseLevel = Convert.ToString(dr["Japanese_Level"]);
                        model.JobTitleJP = Convert.ToString(dr["JobTitleJP"]);
                        model.JobAddress = Convert.ToString(dr["Job_Address"]);
                        model.IsTrasporationInclude = Convert.ToString(dr["Is_Trasporation_Include"]);
                        modelList.Add(model);
                        model.JobTitleJP = Convert.ToString(dr["Job_Title_JP"]);

                        Job job = new Job();
                    }
                }
            }

            return View(modelList);
        }

        [HttpGet]
        public IActionResult UploadUserToGB()
        {
            IEnumerable<JobSeeker> jobseekers = _jobSeekerRepository.GetAll().Where(x => x.Id > 5400).ToList();
            List<string> foundEmails = new List<string>();
            foreach (var item in jobseekers)
            {
                var user = _userManager.FindByIdAsync(item.ApplicationUserId).Result;

                IResponse response = _aPICallingService.GetUserFromGB(user.Email);
                if (response.Result == "OK" && response.UserInfo.Count() == 0)
                {
                    IResponse response2 = _aPICallingService.CreateUserInGB(item);
                    if (response2.Result == "OK")
                    {
                        foundEmails.Add(user.Email);
                        _emailService.SendEmailGaijinAsync("dhananjay.singh@mtic.co.jp", user.Email);
                    }
                    else
                    {
                    }
                }
                else
                {
                }
            }
            return Ok();
        }
    }
}