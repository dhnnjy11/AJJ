using Ajj.Core.Entities;
using Ajj.Core.Interface;
using Ajj.Extensions;
using Ajj.Interface;
using Ajj.Models.AccountViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Ajj.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ICountryRepository _countryRepository;
        private readonly IProvinceRepository _provinceRepository;
        private readonly ILogger _logger;
        private readonly IPostalCodeRepository _postalCodeRepository;
        private readonly IEmailService _emailService;
        private readonly IEmailConfiguration _emailConfiguration;
        private readonly IRepository<ApplicationUser> _userRepository;
        private readonly IJobSeekerRepository _jobSeekerRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IBusinessStreamRepository _businessStreamRepository;
        private readonly IRepository<VisaCategory> _visaCategoryRepository;
        private readonly IAPICallingService _apiCallingService;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IRepository<CompanyImage> _companyImage;
        private readonly IAutoFillService _autoFillService;
        private readonly IRepository<CompanyUser> _companyUsersRepository;
        private readonly IJobRepository _jobRepository;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IRepository<VisaCategory> visaCategoryRepository,
            RoleManager<IdentityRole> roleManager,
            ICountryRepository countryRepository,
            IProvinceRepository provinceRepository,
            IPostalCodeRepository postalCodeRepository,
            ILogger<AccountController> logger,
            IEmailService emailService,
            IEmailConfiguration emailConfiguration,
            IRepository<ApplicationUser> userRepository,
            IJobSeekerRepository jobSeekerRepository,
            IClientRepository clientRepository,
            IBusinessStreamRepository businessStreamRepository,
            IAPICallingService apiCallingService,
            IHostingEnvironment hostingEnvironment,
            IRepository<CompanyImage> companyImage,
            IAutoFillService autoFillService,
            IRepository<CompanyUser> companyUsersRepository,
            IJobRepository jobRepository
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _countryRepository = countryRepository;
            _visaCategoryRepository = visaCategoryRepository;
            _provinceRepository = provinceRepository;
            _logger = logger;
            _postalCodeRepository = postalCodeRepository;
            _emailService = emailService;
            _emailConfiguration = emailConfiguration;
            _userRepository = userRepository;
            _jobSeekerRepository = jobSeekerRepository;
            _clientRepository = clientRepository;
            _businessStreamRepository = businessStreamRepository;
            _apiCallingService = apiCallingService;
            _hostingEnvironment = hostingEnvironment;
            _companyImage = companyImage;
            _autoFillService = autoFillService;
            _companyUsersRepository = companyUsersRepository;
            _jobRepository = jobRepository;
        }

        [TempData]
        public string ErrorMessage { get; set; }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> LoginClient(string returnUrl = null)
        {
            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> LoginAdmin(string returnUrl = null)
        {
            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginClient(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    var user = await _userManager.FindByNameAsync(model.Email);
                    if (user == null)
                    {
                        user = await _userManager.FindByEmailAsync(model.Email);
                    }
                    if (await _userManager.IsInRoleAsync(user, "client") || await _userManager.IsInRoleAsync(user, "client-editor"))
                    {
                        var client = _clientRepository.GetClientByUserId(user.Id);
                        client.Status = 'A';
                        _clientRepository.Update(client);
                        _logger.LogInformation("User logged in.");
                        //return RedirectToLocal(returnUrl);
                        return Redirect("/clients");
                    }
                    else
                    {
                        TempData["error"] = "Do not authorized to access";
                        return View(model);
                    }

                    //return RedirectToLocal(returnUrl);
                }

                if (result.IsNotAllowed)
                {
                    TempData["error"] = "Do not authorized to access, please open email and click on link for verification";
                    return View(model);
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToAction(nameof(LoginWith2fa), new { returnUrl, model.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return RedirectToAction(nameof(Lockout));
                }
                else
                {
                    TempData["error"] = "Invalid login attempt.";
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginAdmin(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    var user = await _userManager.FindByEmailAsync(model.Email);
                    if (await _userManager.IsInRoleAsync(user, "admin"))
                    {
                        _logger.LogInformation("User logged in.");
                        //return RedirectToLocal(returnUrl);
                        return Redirect("/admin");
                    }
                    else
                    {
                        TempData["error"] = "Do not authorizeded to access";
                    }
                }
                else
                {
                    TempData["error"] = "User Name or Password is Incorrect";
                }
            }
            else
            {
                TempData["error"] = "Please enter Username or Password";
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    var user = await _userManager.FindByNameAsync(model.Email);
                    if (await _userManager.IsInRoleAsync(user, "candidate"))
                    {
                        _logger.LogInformation("User logged in.");

                        return RedirectToLocal(returnUrl);
                    }
                    else
                    {
                        ViewData["message"] = "Do not authorizeded to access";
                        return View(model);
                    }
                }
                if (result.IsNotAllowed)
                {
                    ModelState.AddModelError(string.Empty, "Please confirm you email address by clicking on the link, that sent in you email");
                    return View(model);

                }

                if (result.RequiresTwoFactor)
                {
                    return RedirectToAction(nameof(LoginWith2fa), new { returnUrl, model.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return RedirectToAction(nameof(Lockout));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> LoginWith2fa(bool rememberMe, string returnUrl = null)
        {
            // Ensure the user has gone through the username & password screen first
            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();

            if (user == null)
            {
                throw new ApplicationException($"Unable to load two-factor authentication user.");
            }

            var model = new LoginWith2faViewModel { RememberMe = rememberMe };
            ViewData["ReturnUrl"] = returnUrl;

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginWith2fa(LoginWith2faViewModel model, bool rememberMe, string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var authenticatorCode = model.TwoFactorCode.Replace(" ", string.Empty).Replace("-", string.Empty);

            var result = await _signInManager.TwoFactorAuthenticatorSignInAsync(authenticatorCode, rememberMe, model.RememberMachine);

            if (result.Succeeded)
            {
                _logger.LogInformation("User with ID {UserId} logged in with 2fa.", user.Id);
                return RedirectToLocal(returnUrl);
            }
            else if (result.IsLockedOut)
            {
                _logger.LogWarning("User with ID {UserId} account locked out.", user.Id);
                return RedirectToAction(nameof(Lockout));
            }
            else
            {
                _logger.LogWarning("Invalid authenticator code entered for user with ID {UserId}.", user.Id);
                ModelState.AddModelError(string.Empty, "Invalid authenticator code.");
                return View();
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> LoginWithRecoveryCode(string returnUrl = null)
        {
            // Ensure the user has gone through the username & password screen first
            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                throw new ApplicationException($"Unable to load two-factor authentication user.");
            }

            ViewData["ReturnUrl"] = returnUrl;

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginWithRecoveryCode(LoginWithRecoveryCodeViewModel model, string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                throw new ApplicationException($"Unable to load two-factor authentication user.");
            }

            var recoveryCode = model.RecoveryCode.Replace(" ", string.Empty);

            var result = await _signInManager.TwoFactorRecoveryCodeSignInAsync(recoveryCode);

            if (result.Succeeded)
            {
                _logger.LogInformation("User with ID {UserId} logged in with a recovery code.", user.Id);
                return RedirectToLocal(returnUrl);
            }
            if (result.IsLockedOut)
            {
                _logger.LogWarning("User with ID {UserId} account locked out.", user.Id);
                return RedirectToAction(nameof(Lockout));
            }
            else
            {
                _logger.LogWarning("Invalid recovery code entered for user with ID {UserId}", user.Id);
                ModelState.AddModelError(string.Empty, "Invalid recovery code entered.");
                return View();
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Lockout()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IEnumerable<string> getPostalCodeDetail(string postalcode = null)
        {
            List<string> postaldetail = new List<string>();
            //PostalCode postalcodedtl = _postalCodeRepository.GetAll().SingleOrDefault(x => x.Code == postalcode);
            PostalCode postalcodedtl = _postalCodeRepository.GetPostalCodeDetail(postalcode);
            if (postalcodedtl != null)
            {
                postaldetail.Add(postalcodedtl.CityName);
                postaldetail.Add(Convert.ToString(postalcodedtl.ProvinceID));
                postaldetail.Add(postalcodedtl.Town);
            }

            return postaldetail;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Register(string returnUrl = null)
        {
            var model = new RegisterViewModel();
            //model.ProvinceID = 0;

            //model.Countries = new List<SelectListItem>();
            //model.Countries.Add(new SelectListItem
            //{
            //    Value = "",
            //    Text = "--Choose--"
            //});
            //var countries = _countryRepository.GetAll();
            //foreach (var country in countries)
            //{
            //    model.Countries.Add(new SelectListItem
            //    {
            //        Value = Convert.ToString(country.Id),
            //        Text = country.Name
            //    });
            //}

            //model.Provinces = new List<SelectListItem>();
            model.Countries = await _autoFillService.AddCountryListAsync();
            model.Provinces = await _autoFillService.AddProvinceListAsync();
            model.VisaTypes = _autoFillService.AddVisaList();
            //model.Provinces.Add(new SelectListItem
            //{
            //    Value = "",
            //    Text = "--Choose--"
            //});

            //var provinces = _provinceRepository.GetAll();

            //foreach (var province in provinces)
            //{
            //    model.Provinces.Add(new SelectListItem
            //    {
            //        Value = Convert.ToString(province.Id),
            //        Text = province.Name
            //    });
            //}

            //model.VisaTypes.Add(new SelectListItem
            //{
            //    Value = "",
            //    Text = "--Choose Visa--"
            //});

            //var visaList = _visaCategoryRepository.GetAll().Select(x =>
            //new
            //{
            //    x.ParentId,
            //    Name = x.Name.Trim()
            //})
            //.Distinct();

            //foreach (var visa in visaList)
            //{
            //    model.VisaTypes.Add(new SelectListItem
            //    {
            //        Value = Convert.ToString(visa.ParentId),
            //        Text = visa.Name
            //    });
            //}

            ViewData["ReturnUrl"] = returnUrl;
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult RegisterClient(string returnUrl = null)
        {
            var model = new RegisterClientViewModel();
            model.Provinces = _autoFillService.AddProvinceListAsync().Result;

            ViewData["ReturnUrl"] = returnUrl;
            return View(model);
        }

        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> RegisterClient2(RegisterClientViewModel model, string returnUrl = null)
        //{
        //    ViewData["ReturnUrl"] = returnUrl;

        //    if (model.Provinces.Count == 0)
        //    {
        //        model.Provinces = new List<SelectListItem>();
        //        model.Provinces.Add(new SelectListItem
        //        {
        //            Value = "",
        //            Text = "--Choose--"
        //        });

        //        var provinces = _provinceRepository.GetAll();

        //        foreach (var province in provinces)
        //        {
        //            model.Provinces.Add(new SelectListItem
        //            {
        //                Value = Convert.ToString(province.Id),
        //                Text = province.Name_Jp
        //            });
        //        }
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        //email is username
        //        var user = new ApplicationUser { UserName = model.CompanyEmail, PhoneNumber = model.PhoneNumber, Email = model.CompanyEmail, CreateDate = DateTime.Now };

        //        var postalcode = _postalCodeRepository.GetPostalCodeDetail(model.PostalAddrss1 + "-" + model.PostalAddrss2);

        //        var businessStream = new BusinessStream();

        //        var client = new Client { CompanyName = model.CompanyName, Address = model.Address, ApplicationUser = user, ContactEmail = model.CompanyEmail, WebsiteUrl = model.WebsiteUrl, ContactPerson = model.ContactPerson, ContactNumber = model.PhoneNumber, PostalCode = postalcode, Businessstream = businessStream, Status = 'A' };

        //        if (!await _roleManager.RoleExistsAsync("client")) //Check in case of role don't exists
        //        {
        //            var create = await _roleManager.CreateAsync(new IdentityRole("client"));
        //            if (create.Succeeded)
        //            {
        //                _logger.LogInformation("User Role is created ");
        //            }
        //        }

        //        var result = await _userManager.CreateAsync(user, model.Password);
        //        if (result.Succeeded)
        //        {
        //            var isSuccess = _clientRepository.Create(client);
        //            if (isSuccess > 0)
        //            {
        //                var roleRegister = await _userManager.AddToRoleAsync(user, "client");
        //                if (model.Files != null)
        //                {
        //                    //string webrootpath = _hostingEnvironment.WebRootPath;
        //                    string randomName = Guid.NewGuid().ToString().Split('-')[0]; //get random name
        //                    string folderpath = Path.Combine(_hostingEnvironment.WebRootPath, "assets", "img", "companies", client.Id.ToString());
        //                    string virtualPath = Path.Combine("..", "assets", "img", "companies", client.Id.ToString());
        //                    if (!Directory.Exists(folderpath))
        //                    {
        //                        Directory.CreateDirectory(folderpath);
        //                    }
        //                    string filePath = Path.Combine(folderpath, $"companyimage.png");
        //                    string virtualFilePath = Path.Combine(virtualPath, $"companyimage.png");
        //                    using (Image img = Image.FromStream(model.Files[0].OpenReadStream()))
        //                    {
        //                        var resizedImg = img.Resize(250, 250);
        //                        resizedImg.SaveIntoDisk(filePath);
        //                        //save image path into comapnyimage object

        //                        CompanyImage companyImage = new CompanyImage();
        //                        companyImage.ImagePath = virtualFilePath;
        //                        companyImage.Client = client;
        //                        await _companyImage.AddAsyn(companyImage);
        //                    }
        //                }
        //                if (roleRegister.Succeeded)
        //                {
        //                    _logger.LogInformation("Role is added to user");
        //                }
        //            }

        //            _logger.LogInformation("User created a new account with password.");
        //            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        //            var callbackUrl = Url.EmailConfirmationLink(user.Id, code, Request.Scheme);
        //            var domainName = HttpContext.Request.Host.Value;
        //            string[] companyPersons = model.ContactPerson.Split(",");
        //            string[] companyEmails = model.CompanyEmail.Split(",");
        //            List<EmailAddress> emailList = new List<EmailAddress>();
        //            for (int i = 0; i < companyEmails.Length; i++)
        //            {
        //                //emailList.Add(new EmailAddress { Name = companyPersons[i].Trim(), Address = companyEmails[i].Trim() });
        //                _emailService.SendEmailConfirmationClientAsync(companyEmails[i], callbackUrl, domainName, model);
        //            }

        //            _logger.LogInformation("User created a new account with password.");
        //            return Redirect("RegisterClientSuccess");
        //        }
        //        AddErrors(result);
        //    }

        //    return View(model);
        //}


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterClient(RegisterClientViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            model.Provinces = await _autoFillService.AddProvinceListAsync();

            if (ModelState.IsValid)
            {
                //email is username
                var postalcode = _postalCodeRepository.GetPostalCodeDetail(model.PostalAddrss1 + "-" + model.PostalAddrss2);

                var user = new ApplicationUser { FirstName = model.FirstName, LastName = model.LastName, UserName = model.CompanyEmail, PhoneNumber = model.PhoneNumber, Email = model.CompanyEmail, CreateDate = DateTime.Now };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var personName = model.LastName + " " +model.FirstName;
                    var client = new Client { CompanyName = model.CompanyName, Address = model.Address, ContactEmail = model.CompanyEmail, WebsiteUrl = model.WebsiteUrl, ContactPerson = personName, ContactNumber = model.PhoneNumber, PostalCode = postalcode, Status = 'A',  };

                    await _clientRepository.AddAsyn(client); //create company details

                    if (!await _roleManager.RoleExistsAsync("client")) //Check in case of role don't exists
                    {
                        var create = await _roleManager.CreateAsync(new IdentityRole("client"));
                        if (create.Succeeded)
                        {
                            _logger.LogInformation("User Role is created ");
                        }
                    }
                    await _userManager.AddToRoleAsync(user, "client"); //add client role to user

                    var companyusermap = new CompanyUser
                    {
                        Client = client,
                        ApplicationUser = user
                    };
                    await _clientRepository.SaveAsync();
                    _companyUsersRepository.Create(companyusermap);

                    //add company image in profile
                    if (model.Files != null)
                    {
                        //string webrootpath = _hostingEnvironment.WebRootPath;
                        string randomName = Guid.NewGuid().ToString().Split('-')[0]; //get random name
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

                            CompanyImage companyImage = new CompanyImage();
                            companyImage.ImagePath = virtualFilePath;
                            companyImage.Client = client;
                            await _companyImage.AddAsyn(companyImage);
                        }
                    }

                    _logger.LogInformation("User created a new account with password.");
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.EmailConfirmationLink(user.Id, code, Request.Scheme);
                    var domainName = HttpContext.Request.Host.Value;
                    string[] companyPersons = model.ContactPerson.Split(",");
                    string[] companyEmails = model.CompanyEmail.Split(",");
                    List<EmailAddress> emailList = new List<EmailAddress>();
                    for (int i = 0; i < companyEmails.Length; i++)
                    {
                        //emailList.Add(new EmailAddress { Name = companyPersons[i].Trim(), Address = companyEmails[i].Trim() });
                        _emailService.SendEmailConfirmationClientAsync(companyEmails[i], callbackUrl, domainName, model);
                    }

                    _logger.LogInformation("User created a new account with password.");
                    return Redirect("RegisterClientSuccess");
                }

                AddErrors(result);
            }

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                model.Countries = await _autoFillService.AddCountryListAsync();
                model.Provinces = await _autoFillService.AddProvinceListAsync();

                var user = new ApplicationUser { UserName = model.Email, Email = model.Email, PhoneNumber = model.PhoneNumber, CreateDate = DateTime.Now };

                var country = _countryRepository.GetById(model.CountryID);
                var visacategory = _visaCategoryRepository.GetById(model.VisaTypeId);

                var province = _provinceRepository.GetById(model.ProvinceID);

                var jobseeker = new JobSeeker { Age = model.Age, BirthYear = model.BirthYear, BirthMonth = model.BirthMonth, BirthDay = model.BirthDay, ProvinceID = model.ProvinceID, Country = country, Address = model.Address, City = model.CityName, Town = model.Town, InJapan = model.InJapan, FirstName = model.FirstName, LastName = model.LastName, OtherCountry = model.OtherCountry, Gender = model.Gender, PostalAddrss = model.PostalAddrss1 + "-" + model.PostalAddrss2, Visa = model.Visa, Origin = "AJJ", IsAgreed = model.IsAgreed, VisaCategory = visacategory, IsPermitToWork = model.IsPermitToWork, SubVisaType = model.SubVisaType, ApplicationUser = user };

                if (!await _roleManager.RoleExistsAsync("candidate"))
                {
                    var create = await _roleManager.CreateAsync(new IdentityRole("candidate"));
                    if (create.Succeeded)
                    {
                        _logger.LogInformation("User Role is created ");
                    }
                }
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    var roleRegister = await _userManager.AddToRoleAsync(user, "candidate");
                    if (roleRegister.Succeeded)
                    {
                        int isSuccess = _jobSeekerRepository.Create(jobseeker);
                        if (isSuccess > 0)
                        {
                            _logger.LogInformation("Jobseeker successfully registered");
                        }

                        _logger.LogInformation("Role is added to user");
                    }
                    _logger.LogInformation("User created a new account with password.");
                    string candidatename = model.FirstName;
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.EmailConfirmationLink(user.Id, code, Request.Scheme);
                    _emailService.SendEmailConfirmationAsync(model.Email, candidatename, callbackUrl);

                    _logger.LogInformation("User created a new account with password.");
                    return Redirect("RegisterSuccess");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            //bind countries and provinces
            //if (model.Countries == null)
            //{
            //    model.Countries = new List<SelectListItem>();
            //    model.Countries.Add(new SelectListItem
            //    {
            //        Value = "",
            //        Text = "--Choose--"
            //    });
            //    var countries = _countryRepository.GetAll();
            //    foreach (var country in countries)
            //    {
            //        model.Countries.Add(new SelectListItem
            //        {
            //            Value = Convert.ToString(country.Id),
            //            Text = country.Name
            //        });
            //    }
            //}
            //if (model.Provinces == null)
            //{
            //    model.Provinces = new List<SelectListItem>();
            //    model.Provinces.Add(new SelectListItem
            //    {
            //        Value = "",
            //        Text = "--Choose--"
            //    });

            //    var provinces = _provinceRepository.GetAll();

            //    foreach (var province in provinces)
            //    {
            //        model.Provinces.Add(new SelectListItem
            //        {
            //            Value = Convert.ToString(province.Id),
            //            Text = province.Name
            //        });
            //    }
            //}

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout(string role)
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            if (role == "admin")
            {
                return RedirectToAction("Index", "Admin", new { area = "area" });
            }
            if (role == "client")
            {
                return RedirectToAction("IndexAsync", "Job", new { area = "clients" });
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult ExternalLogin(string provider, string returnUrl = null)
        {
            // Request a redirect to the external login provider.
            var redirectUrl = Url.Action(nameof(ExternalLoginCallback), "Account", new { returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return Challenge(properties, provider);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        {
            if (remoteError != null)
            {
                ErrorMessage = $"Error from external provider: {remoteError}";
                return RedirectToAction(nameof(Login));
            }
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return RedirectToAction(nameof(Login));
            }

            // Sign in the user with this external login provider if the user already has a login.
            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
            if (result.Succeeded)
            {
                _logger.LogInformation("User logged in with {Name} provider.", info.LoginProvider);
                return RedirectToLocal(returnUrl);
            }
            if (result.IsLockedOut)
            {
                return RedirectToAction(nameof(Lockout));
            }
            else
            {
                // If the user does not have an account, then ask the user to create an account.
                ViewData["ReturnUrl"] = returnUrl;
                ViewData["LoginProvider"] = info.LoginProvider;
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);
                return View("ExternalLogin", new ExternalLoginViewModel { Email = email });
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExternalLoginConfirmation(ExternalLoginViewModel model, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await _signInManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    throw new ApplicationException("Error loading external login information during confirmation.");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email, EmailConfirmed = true };
                var result = await _userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await _userManager.AddLoginAsync(user, info);
                    if (result.Succeeded)
                    {
                        result = await _userManager.AddToRoleAsync(user, "candidate");
                        if (result.Succeeded)
                        {

                            await _signInManager.SignInAsync(user, isPersistent: false);
                            _logger.LogInformation("User created an account using {Name} provider.", info.LoginProvider);
                            return RedirectToLocal(returnUrl);
                        }

                    }
                }
                AddErrors(result);
            }

            ViewData["ReturnUrl"] = returnUrl;
            return View(nameof(ExternalLogin), model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{userId}'.");
            }

            if (user.EmailConfirmed == false)
            {
                var role = _userManager.GetRolesAsync(user).Result.SingleOrDefault();
                var result = await _userManager.ConfirmEmailAsync(user, code);
                //_emailService.SendEmailAdminAsync("dhananjay.singh011@gmail.com", user.Email);
                _emailService.SendEmailAdminAsync("registration-notice@jobsjapan.net", user.Email, role);

                var jobseeker = _jobSeekerRepository.GetJobSeekerByUserId(user.Id);
                IResponse response = _apiCallingService.CreateUserInGB(jobseeker);
                await GetRecommendedJobsAsync(user); //send recommended jobs to the candidate.
                if (response == null || response.Result != "OK")
                {
                    _emailService.SendErrorEmailAsync("dhananjay.singh@mtic.co.jp", response.Error, "Create", user.Email);
                }
                else if (response.Result == "OK")
                {
                    _emailService.SendEmailGaijinAsync("registration-notice@jobsjapan.net", user.Email);
                }

                return View(result.Succeeded ? "ConfirmEmail" : "Error");
            }
            else
            {
                return Redirect("/Home/Index");
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return RedirectToAction(nameof(ForgotPasswordConfirmation));
                }

                // For more information on how to enable account confirmation and password reset please
                // visit https://go.microsoft.com/fwlink/?LinkID=532713
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                var callbackUrl = Url.ResetPasswordCallbackLink(user.Id, code, Request.Scheme);
                //await _emailSender.SendEmailAsync(model.Email, "Reset Password",
                // $"Please reset your password by clicking here: <a href='{callbackUrl}'>link</a>");
                await _emailService.SendEmailAsync(model.Email, "Reset Password",
                   $"Please reset your password by clicking here: <a href='{callbackUrl}'>link</a>");
                return RedirectToAction(nameof(ForgotPasswordConfirmation));
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string code = null)
        {
            if (code == null)
            {
                throw new ApplicationException("A code must be supplied for password reset.");
            }
            var model = new ResetPasswordViewModel { Code = code };
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction(nameof(ResetPasswordConfirmation));
            }
            var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
            if (result.Succeeded)
            {
                IResponse response = _apiCallingService.ResetPasswordInGB(user.Email, model.Password);
                if (response.Result != "OK")
                {
                    _emailService.SendErrorEmailAsync("dhananjay.singh011@gmail.com", response.Error, "ChangePassword", user.Email);
                }
                return RedirectToAction(nameof(ResetPasswordConfirmation));
            }
            AddErrors(result);
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        [Route("Client/SetPassword")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> SetPasswordAsync(string userId, string code = null)
        {
            if (code == null)
            {
                throw new ApplicationException("A code must be supplied for password reset.");
            }
            var user = await _userManager.FindByIdAsync(userId);
            user.EmailConfirmed = true;
            await _userManager.UpdateAsync(user);

            var model = new ResetPasswordViewModel { Email = user.Email, Code = code };
            return View(model);
        }

        [Route("Client/SetPassword")]
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SetPasswordAsync(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction(nameof(SetPasswordConfirmation));
            }
            var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
            if (result.Succeeded)
            {

                var client = _clientRepository.GetClientByUserId(user.Id);
                client.Status = 'A';
                _clientRepository.Update(client);
                return RedirectToAction(nameof(SetPasswordConfirmation));
            }
            AddErrors(result);
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("Client/SetPasswordConfirmation")]
        public IActionResult SetPasswordConfirmation()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

        #region Helpers

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult RegisterSuccess()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult RegisterClientSuccess()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult TestEmail()
        {
            var user = _userManager.FindByEmailAsync("dhananjay.singh@mtic.co.in");

            EmailAddress toEmailAddress = new EmailAddress()
            {
                Name = "Dhananjay",
                Address = "dhananjay.singh011@gmail.com"
            };

            dynamic emails =
                   JsonConvert.DeserializeObject(System.IO.File.ReadAllText("Formats/emails.json"));
            var emailformat = emails["clientcredential"];

            EmailMessage emailMessage = new EmailMessage()
            {
                ToAddresses = new List<EmailAddress>() {
                    toEmailAddress
                },
                Subject = emailformat["subject"].Value,
                Content = emailformat["content"].Value
            };
            _emailService.Send(emailMessage);
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public bool VerifyUserName(string username)
        {
            var user = _userRepository.Find(x => x.UserName == username);
            //var user = _userManager.FindByNameAsync(username);
            if (user.Count() == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        [AllowAnonymous]
        public IActionResult SendSpecificMail()
        {
            // _emailService.SendSpecificMailAsync("");
            return Ok("success");
        }

        [HttpGet]
        [Authorize]
        public async Task GetRecommendedJobsAsync(ApplicationUser user)
        {
            string basequery = $"select * from ajjjob";
            string conditionquery = $"where";
            var preferredJobs = new List<Job>();
            //var user = await _userManager.GetUserAsync(HttpContext.User);
            var jobseeker = _jobSeekerRepository.GetJobSeekerByUserId(user.Id);
            char jobAllowStatus = jobseeker.VisaCategory.AllowJobStatus;
            int.TryParse(jobseeker.Age, out int userAge); //age of jobseeker
            string userProvince = jobseeker.Province.Name;//prefrecture of jobseeker
            conditionquery += $" provinceName like '%{userProvince}%'";
            var businessStreams = await _businessStreamRepository.GetAllowedCategoryAsync((int)jobseeker.VisaCategoryId, jobAllowStatus);
            int[] arry = businessStreams.Select(x => x.Id).ToArray();
            var postalCode = _postalCodeRepository.GetPostalCodeDetail(jobseeker.PostalAddrss);
            string jobseekerCity = postalCode.CityName_En;
            if (string.IsNullOrWhiteSpace(jobseekerCity))
            {
                jobseekerCity = jobseeker.City;
            }
            if (jobseeker.JobSkills.Count() > 0)
            {

                var jobSkills = jobseeker.JobSkills;
                int[] arryPreference = jobSkills.Select(x => x.BusinessStreamId).ToArray();
                arry = arry.Intersect(arryPreference).ToArray();
                //if (arryPreference.Length > 0)
                //{
                //    var newCommaSepratedString = string.Join(",", arryPreference);
                //    conditionquery += $" and BusinessStreamID in ({newCommaSepratedString})";
                //}
            }

            if (arry.Length > 0)
            {
                var newCommaSepratedString = string.Join(",", arry);
                conditionquery += $" and BusinessStreamID in ({newCommaSepratedString})";
            }

            if (jobAllowStatus == 'T') // if user only allowed to time bounded jobs
            {
                conditionquery += $" and ContractType_Text !=  'full-time'";
            }
            if (userAge != 0) //if maximum and minimum age requirment is given in job
            {
                conditionquery += $" and MaxAge >= {userAge} >= MinAge";
            }


            string query = basequery + " " + conditionquery;

            var jobs = _jobRepository.GetJobsDynamic(query)
                                     .Where(x => x.PostalCode.CityName_En == jobseekerCity)
                                     .Take(5);

            jobs = jobs.Concat(_jobRepository.GetJobsDynamic(query)
                                     .Take(4)
                                     .OrderByDescending(x => x.PostDate));


            if (jobs.Count() == 0) //In case of job is not available in current city
            {

                if (jobs.Count() == 0)
                {
                    return;
                }

            }


            string domainName = $"http://www.jobsjapan.net";

            StringBuilder sb = new StringBuilder();
            sb.Append("<body> <div style='font-family:Arial' class='email-body'> <div class='email-header' style='background:linear-gradient(#FF4F57, #e6595f); padding:10px;color:white;'> Recommended jobs for you </div> <div class='email-content' style='border:1px solid #e6595f; padding:10px;'> ");
            foreach (var job in jobs)
            {
                var postal = _postalCodeRepository.GetById(job.PostalCodeId);
                string address = $"{postal.Province.Name.ToUpper()} {postal.CityName_En} {postal.Town_En}";
                string jobLink = $"{domainName}/Job/JobDetails/{job.Id}";
                string jobImgUrl = $"{domainName}{job.BusinessStream.CategoryImageUrl}";
                string salary = "";
                if (!string.IsNullOrWhiteSpace(job.Salary_Hourly))
                {
                    salary = "&#165;" + job.Salary_Hourly + "per hour";
                }
                if (!string.IsNullOrWhiteSpace(job.Salary_Monthly))
                {
                    salary = "&#165;" + job.Salary_Monthly + "per month";
                }
                sb.Append($"<div class='job-category-img' style='float:left;margin:15px 25px 10px 10px'> <img class='img img-responsive job-display__thumbnail' src='{jobImgUrl}' alt='株式会社Ｈarvest Ｂiz Ｃareer'> </div> ");
                sb.Append("<div class='email-jobview' style='border-bottom:1px dashed;margin-top:1rem'>");
                sb.Append($"<span bgcolor='#FF4F57' class='email-tag' style='background-color:#FF4F57; color:white; padding:5px; font-size:12px; border-radius:10px;'>{job.ContractType_Text}</span>");
                sb.Append("&nbsp;");
                sb.Append($"<span class='email-tag' style='background-color:#FF4F57; color:white; padding:5px; font-size:12px; border-radius:10px;'>{job.BusinessStream.Name}</span>");
                sb.Append($"<div style='margin-top:8px;margin-bottom:8px;font-size:14px;'> <a href='{jobLink}' style='text-decoration:none'>{job.JobTitle}</a> </div>");
                sb.Append($" <table style='font-size:0.8rem'> <tr style='border:0.5 dashed'><td style='margin-right:0.6rem'>Salary:</td><td>{salary}</td> </tr> <tr> <td style='padding-right:1.2rem'>Working Hours :</td><td>{job.Workinghour} hours</td> </tr> <tr> <td>Address :</td><td>{address}</td> </tr> <tr> <td></td><td></td> </tr> </table>");
                sb.Append($"<div style='margin-top:1.2rem;margin-bottom:1.2rem'><a href='{jobLink}' style='padding:0.5rem 2rem;font-size:0.9rem;border-radius:0;color:white;background-color:#337ab7;text-decoration:none'>Apply</a> </div>");
                sb.Append("</div>");
            }
            sb.Append("</div><div></body> ");

            await _emailService.SendEmailAsync(user.Email, "Job offers", sb.ToString());


        }

        #endregion Helpers
    }
}