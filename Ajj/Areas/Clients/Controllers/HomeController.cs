using Ajj.Areas.Clients.Models;
using Ajj.Core.Entities;
using Ajj.Core.Interface;
using Ajj.Extensions;
using Ajj.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MTIC.Service.Email;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Ajj.Areas.Clients.Controllers
{
    [Area("Clients")]
    [Route("clients")]
    [Authorize(Roles = "client,client-editor")]
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailService _emailService;
        private readonly IBusinessStreamRepository _businessRepository;
        private readonly IJobRepository _jobRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IPostalCodeRepository _postalCodeRepository;
        private readonly IProvinceRepository _provinceRepository;
        private readonly IJobApplyRepository _jobApplyRepository;
        private readonly IJobSeekerRepository _jobseekerRepository;
        private readonly IRepository<CompanyImage> _companyImage;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IAutoFillService _autoFillService;
        private readonly IRepository<CompanyUser> _companyUser;

        public HomeController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            IEmailService emailService,
            IBusinessStreamRepository businessRepository,
            IJobRepository jobRepository,
            IClientRepository clientRepository,
            IPostalCodeRepository postalCodeRepository,
            IProvinceRepository provinceRepository,
            IJobApplyRepository jobApplyRepository,
            IJobSeekerRepository jobseekerRepository,
            IRepository<CompanyImage> companyImage,
            IHostingEnvironment hostingEnvironment,
            IAutoFillService autoFillService,
            IRepository<CompanyUser> companyUser
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _emailService = emailService;
            _businessRepository = businessRepository;
            _jobRepository = jobRepository;
            _clientRepository = clientRepository;
            _postalCodeRepository = postalCodeRepository;
            _provinceRepository = provinceRepository;
            _jobApplyRepository = jobApplyRepository;
            _jobseekerRepository = jobseekerRepository;
            _companyImage = companyImage;
            _hostingEnvironment = hostingEnvironment;
            _autoFillService = autoFillService;
            _companyUser = companyUser;
        }

        [HttpGet]
        [Route("Home/ChangePassword")]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [Route("Home/ChangePassword")]
        public async Task<IActionResult> ChangePassword(ClientAccountViewModel model)
        {
            if (ModelState.IsValid)
            {
                //var user = await _userManager.FindByNameAsync(model.UserName);
                var user = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);

                if (user != null)
                {
                    var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.Password);
                    if (result.Succeeded)
                    {
                        ViewData["message"] = "パスワード変更に成功しました";
                        return View("ChangePassword");
                    }
                    else
                    {
                        ViewData["message"] = "User Name or Password is incorrect";
                    }
                }
            }

            return View();
        }

        [HttpGet]
        [Route("Home/ChangeUserName")]
        public IActionResult ChangeUserName()
        {
            ChangeUserNameViewModel model = new ChangeUserNameViewModel();
            var userName = _userManager.GetUserName(HttpContext.User);
            model.UserName = userName;

            return View(model);
        }

        [HttpPost]
        [Route("Home/ChangeUserName")]
        [Authorize(Roles = "client")]
        public IActionResult ChangeUserName(ChangeUserNameViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _userManager.GetUserAsync(HttpContext.User).Result;

                //var user = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);

                if (user != null)
                {
                    var result = _userManager.SetUserNameAsync(user, model.UserName).Result;
                    if (result.Succeeded)
                    {
                        ViewData["message"] = "User Changed Successfully";
                        return View();
                    }
                    else
                    {
                        foreach (var err in result.Errors)
                        {
                            string errDesc = err.Description;
                            ModelState.AddModelError(string.Empty, errDesc);
                        }

                        //ViewData["message"] = "Error in changing Username";
                    }
                }
            }

            return View();
        }

        [HttpGet]
        [Route("Home/EditProfile")]
        public async Task<IActionResult> EditProfile()
        {
            var model = new ClientEditViewModel();
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var userId = _userManager.GetUserId(HttpContext.User);
            var username = _userManager.GetUserName(HttpContext.User);
            if (userId != null)
            {
                var client = _clientRepository.GetClientByUserId(userId);

                model.Provinces = await _autoFillService.AddProvinceListAsync();
                model.FirstName = user.FirstName;
                model.PhoneNumber = user.PhoneNumber;
                model.LastName = user.LastName;
                model.ClientID = client.Id;
                model.CompanyName = client.CompanyName;
                //model.ContactPerson = client.ContactPerson;
                model.ClientID = client.Id;
                model.UserName = username;
                model.WebsiteUrl = client.WebsiteUrl;
                model.ContactPerson = client.ContactPerson;
                //model.PhoneNumber = client.ContactNumber;
                model.BusinessStreamID = client.BusinessstreamID;

                PostalCode postalcode = _postalCodeRepository.GetById(client.PostalCodeID);

                string[] postalcodeArry = postalcode.Code.Split("-");
                if (postalcodeArry.Count() > 0)
                {
                    model.PostalAddrss1 = postalcodeArry[0];
                    model.PostalAddrss2 = postalcodeArry[1];
                }
                model.ProvinceID = postalcode.ProvinceID;
                model.Address = client.Address;
                model.CityName = client.PostalCode.CityName;
                model.Town = client.PostalCode.Town;
                //}
            }
            return View(model);
        }

        [HttpPost]
        [Route("Home/EditProfile")]
        public async Task<IActionResult> EditProfile(ClientEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _userManager.GetUserAsync(HttpContext.User).Result;
                model.Provinces = await _autoFillService.AddProvinceListAsync();
                int postalId = 0;
                string postalcode = model.PostalAddrss1 + "-" + model.PostalAddrss2;
                var postalcodeList = _postalCodeRepository.Find(x => x.Code == postalcode);
                foreach (var postalcodeobj in postalcodeList)
                {
                    postalId = postalcodeobj.Id;
                }
                Client client = _clientRepository.GetClientByUserId(user.Id);



                //client.Id = model.ClientID;
                client.CompanyName = model.CompanyName;
                client.ContactNumber = model.PhoneNumber;
                client.ContactPerson = model.ContactPerson;
                client.WebsiteUrl = model.WebsiteUrl;
                client.PostalCodeID = postalId;
                client.Address = model.Address;
                //client.ApplicationUser = user;

                if (model.Files != null)
                {
                    //string webrootpath = _hostingEnvironment.WebRootPath;                    
                    string folderpath = Path.Combine(_hostingEnvironment.WebRootPath, "assets", "img", "companies", client.Id.ToString());
                    string virtualPath = Path.Combine("..", "assets", "img", "companies", client.Id.ToString());
                    if (!Directory.Exists(folderpath))
                    {
                        Directory.CreateDirectory(folderpath);
                    }
                    string filePath = Path.Combine(folderpath, $"companyimage.png");
                    string virtualFilePath = Path.Combine(virtualPath, $"companyimage.png");
                    using (Image img = Image.FromStream(model.Files[0].OpenReadStream()))
                    {
                        var resizedImg = img.Resize(250, 250);
                        resizedImg.SaveIntoDisk(filePath);
                        //save image path into comapnyimage object

                        IEnumerable<CompanyImage> companyImages = await _companyImage.FindByAsyn(x => x.ClientId == model.ClientID);
                        CompanyImage companyImage = new CompanyImage();
                        if (companyImages.Count() < 1)
                        {

                            companyImage.ImagePath = virtualFilePath;
                            companyImage.Client = client;
                            await _companyImage.AddAsyn(companyImage);
                        }
                        else
                        {
                            companyImage = companyImages.FirstOrDefault();
                            companyImage.ImagePath = virtualFilePath;
                            _companyImage.Update(companyImage);
                        }

                    }
                }

                var result = _clientRepository.Update(client);

                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.PhoneNumber = model.PhoneNumber;
                // await _companyImage.SaveAsync();

                //Saving company image
                //long size = model.Files.Sum(f => f.Length);

                //End of company save image

                if (result > 0)
                {
                    ModelState.Clear();
                    ViewData["success"] = "データが保存されました";
                }
                else
                {
                    //foreach (var error in ModelState)
                    //{
                    //    ModelState.AddModelError(string.Empty, error.Description);
                    //}
                    ViewData["error"] = "データ保存の際にエラーが発生しました";
                }
            }
            return View(model);
        }

        [HttpGet]
        [Route("Home/AddNewUser"), Authorize(Roles = "client")]
        public async Task<IActionResult> AddNewUserAsync()
        {
            NewUserViewModel model = new NewUserViewModel();

            model.Provinces = await _autoFillService.AddProvinceListAsync();
            return View(model);
        }

        [HttpPost]
        [Route("Home/AddNewUser"), Authorize(Roles = "client")]
        public async Task<IActionResult> AddNewUserAsync(NewUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.Provinces = await _autoFillService.AddProvinceListAsync();
                //For new user
                ApplicationUser newuser = new ApplicationUser();
                newuser.Email = model.Email;
                newuser.UserName = model.Email;
                newuser.PhoneNumber = model.Phone;
                newuser.FirstName = model.FirstName;
                newuser.LastName = model.LastName;
                int.TryParse(model.ProvinceID, out int ProvinceId);
                newuser.ProvinceId = ProvinceId;
                newuser.CityName = model.CityName;
                newuser.Town = model.Town;
                newuser.StreetAddress = model.Address;
                //user.PostalCode = model.PostalAddrss1 + "-" + model.PostalAddrss2;
                var appUser = await _userManager.GetUserAsync(HttpContext.User);

                var company = _clientRepository.GetClientByUserId(appUser.Id);
                if (!_roleManager.RoleExistsAsync("client-editor").Result)
                {
                    await _roleManager.CreateAsync(new IdentityRole("client-editor"));
                }

                var companyUser = new CompanyUser { Client = company, ApplicationUser = newuser };
                var result = await _userManager.CreateAsync(newuser);
                if (result.Succeeded)
                {
                    result = await _userManager.AddToRoleAsync(newuser, "client-editor");
                    _companyUser.Create(companyUser);
                    var code = await _userManager.GeneratePasswordResetTokenAsync(newuser);
                    var callbackUrl = Url.SetPasswordCallbackLink(newuser.Id, code, Request.Scheme);

                    var domainName = HttpContext.Request.Host.Value;
                    string userName = $"{newuser.FirstName} {newuser.LastName}";
                    List<EmailAddress> emailList = new List<EmailAddress>()
                    {
                        new EmailAddress()
                        {
                            Address = newuser.Email,
                            Name = newuser.LastName + " " + newuser.FirstName
                        }
                    };

                    _emailService.SendEmailNewCompanyMemberAsync(emailList, callbackUrl, domainName, userName, company);

                    ViewData["success"] = "データが保存されました";

                    return View(model);
                }

            }
            return View();
        }
    }
}