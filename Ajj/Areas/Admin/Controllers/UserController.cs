using Ajj.Areas.Admin.Models;
using Ajj.Core.Entities;
using Ajj.Core.Interface;
using Ajj.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace Ajj.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin")]
    [Authorize(Roles = "admin")]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJobApplyRepository _jobApplyRepository;
        private readonly IJobSeekerRepository _jobSeekerRepository;

        public UserController(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IJobApplyRepository jobApplyRepository,
            IJobSeekerRepository jobSeekerRepository)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jobApplyRepository = jobApplyRepository;
            _jobSeekerRepository = jobSeekerRepository;
        }

        [Route("User/All")]
        [HttpGet]
        public IActionResult AllUsersAsync()
        {


            return View();
        }

        [Route("User/JobApplications")]
        [HttpGet]
        public IActionResult JobApplications()
        {
           

            return View();
        }


        [Route("User/GetApplications")]
        [HttpPost]
        public async Task<dynamic> GetApplicationsAsync(DataTableAjaxPostModel model)
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

            var jobapplies = await _jobApplyRepository.GetAllAsyn();
            //convert to anonymous type object list as inummerable
            var applicationList = jobapplies.Select(x =>
           new JobApplicationViewModel()
            {
                JobId = x.Job.Id,
                ApplicantEmail = x.User.Email,
                CompanyName = x.Job.Client.CompanyName,
                ApplyDate = x.ApplyDate.Date.ToString("yyyy/M/dd"),
                JobTitle = x.Job.JobCategory.CategoryName
            });
            //searching 
            totalResultsCount = applicationList.Count();

            if (!string.IsNullOrWhiteSpace(searchBy))
            {
                searchBy = searchBy.ToLower();
                applicationList = applicationList.Where(x => x.JobId.ToString().Contains(searchBy) || x.JobTitle.ToLower().Contains(searchBy) || x.CompanyName.ToLower().Contains(searchBy) || x.ApplyDate.ToString().ToLower().Contains(searchBy) || x.ApplicantEmail.ToLower().Contains(searchBy));

                // if we have an empty search then just order the results by Id ascending
                sortBy = "jobId";
                sortDir = "desc";
            }
            filteredResultsCount = applicationList.Count();
            applicationList = applicationList
                .AsQueryable()
                .OrderBy(sortBy + " " + sortDir)  //sorting 
                .Skip(model.start)
                .Take(model.length)
                ;

            var secondObj = new
            {
                model.draw,
                recordsTotal = totalResultsCount,
                recordsFiltered = filteredResultsCount,
                data = applicationList

            };
            return secondObj;
        }

        [Route("User/GetUsers")]
        [HttpPost]
        public async Task<dynamic> GetUsersAsync(DataTableAjaxPostModel model)
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

            var jobseekers = await _jobSeekerRepository.GetAllAsyn();
            //convert to anonymous type object list as inummerable
            var jobseekerList = jobseekers.Select(x =>
           new 
           {
               UserId = x.Id,
               x.ApplicationUser.Email,
               PhoneNumber = x.ApplicationUser.PhoneNumber ?? "",
               RegisteredDate = x.ApplicationUser.CreateDate.Date,
               Prefecture = x.Province.Name ?? ""
           });
            //searching 
            totalResultsCount = jobseekerList.Count();

            if (!string.IsNullOrWhiteSpace(searchBy))
            {
                searchBy = searchBy.ToLower();
                jobseekerList = jobseekerList.Where(x => x.UserId.ToString().Contains(searchBy) || x.Email.ToLower().Contains(searchBy) || x.PhoneNumber.Contains(searchBy) || x.RegisteredDate.ToString().Contains(searchBy) || x.Prefecture.ToLower().Contains(searchBy));

                // if we have an empty search then just order the results by Id ascending
                sortBy = "userId";
                sortDir = "desc";
            }
            filteredResultsCount = jobseekerList.Count();
            jobseekerList = jobseekerList
                .AsQueryable()
                .OrderBy(sortBy + " " + sortDir)  //sorting 
                .Skip(model.start)
                .Take(model.length)
                ;

            var secondObj = new
            {
                model.draw,
                recordsTotal = totalResultsCount,
                recordsFiltered = filteredResultsCount,
                data = jobseekerList
            };
            return secondObj;
        }

        [Route("User/Register")]
        [HttpGet]
        //[Authorize(Roles = "admin")]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [Route("User/Register")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                //check if user already exists or not
                if (user == null)
                {
                    user = new ApplicationUser { UserName = model.Email, Email = model.Email, PhoneNumber = model.PhoneNumber, EmailConfirmed = true };
                    var result = await _userManager.CreateAsync(user, model.Password);
                    if (!result.Succeeded)
                    {
                        ViewData["message"] = "Error in saving data";

                        return View(model);
                    }
                }
                //check user already registered as admin or not
                if (await _userManager.IsInRoleAsync(user, "admin"))
                {
                    ViewData["message"] = "User already registered as admin";
                    ModelState.Clear();
                    return View();
                }

                string rolename = model.RoleName;
                if (!await _roleManager.RoleExistsAsync(rolename))
                {
                    var create = await _roleManager.CreateAsync(new IdentityRole(rolename));
                    if (create.Succeeded)
                    {
                        //_logger.LogInformation("User Role is created ");
                    }
                }

                var roleRegister = await _userManager.AddToRoleAsync(user, rolename);
                if (roleRegister.Succeeded)
                {
                    ViewData["message"] = "Successfully Save Record";

                    //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    //var callbackUrl = Url.EmailConfirmationLink(user.Id, code, Request.Scheme);
                    //Email sending code
                    //EmailAddress toEmailAddress = new EmailAddress();
                    //toEmailAddress.Name = model.Email;
                    //toEmailAddress.Address = model.Email;
                    //EmailMessage emailMessage = new EmailMessage()
                    //{
                    //    ToAddresses = new List<EmailAddress>() {
                    //toEmailAddress
                    //},
                    //Subject = "Confirm your email",
                    // Content = $"Congratulation! you have successfully registerd with Ajj, Please click on link confirmation : <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>{HtmlEncoder.Default.Encode(callbackUrl).ToString()}</a>"
                    //};
                    // _emailService.Send(emailMessage);
                    return View();
                }
            }

            return View(model);
        }

        [Route("User/ManageRole")]
        [HttpGet]
        //[Authorize(Roles = "admin")]
        [AllowAnonymous]
        public IActionResult ManageRole()
        {
            return View();
        }

        [Route("User/ManageRole")]
        [HttpPost]
        //[Authorize(Roles = "admin")]
        [AllowAnonymous]
        public IActionResult ManageRole(string UserName, string RoleName)
        {
            return View();
        }
    }
}