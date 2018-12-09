using Ajj.Core.Entities;
using Ajj.Core.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MTIC.Service.Email;

namespace Ajj.Areas.Admin.Controllers
{
    //[Area("Admin")]
    //[Route("admin")]
    //[Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IClientRepository _clientRepository;
        private readonly IEmailService _emailService;
        private readonly IBusinessStreamRepository _businessRepository;
        private readonly IProvinceRepository _provinceRepository;

        // private readonly IEmailSender _emailSender;
        private readonly IPostalCodeRepository _postalCodeRepository;

        public AdminController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            IClientRepository clientRepository,
            IEmailService emailService,
            IBusinessStreamRepository businessRepository,
            IProvinceRepository provinceRepository,
            //IEmailSender emailSender
            IPostalCodeRepository postalCodeRepository
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _clientRepository = clientRepository;
            _emailService = emailService;
            _businessRepository = businessRepository;
            //_emailSender = emailSender;
            _provinceRepository = provinceRepository;
            _postalCodeRepository = postalCodeRepository;
        }

        //[HttpGet]
        //[AllowAnonymous]
        //public IActionResult Index()
        //{
        //    if (!_signInManager.IsSignedIn(User))
        //    {
        //        return Redirect("~/Account/LoginAdmin");
        //    }
        //    else
        //    {
        //        System.Security.Claims.ClaimsPrincipal currentUser = User;
        //        var isAdmin = currentUser.IsInRole("admin");

        //        if (isAdmin)
        //        {
        //            var model = _clientRepository.Find(x => x.ApplicationUserID == null);
        //            return View(model);
        //        }
        //    }

        //    return Redirect("~/Account/LoginAdmin");

        //}

        //public IActionResult Clients()
        //{
        //    return View();
        //}

        //[Route("Client/Potential")]
        //[HttpGet]
        //public IActionResult PotentialClients()
        //{
        //    if (!_signInManager.IsSignedIn(User))
        //    {
        //        return Redirect("~/Account/LoginAdmin");
        //    }
        //    var model = _clientRepository.Find(x => x.ApplicationUserID == null);
        //    return View(model);
        //}

        //[Route("Client/RegisteredClients")]
        //[HttpGet]
        //public IActionResult RegisteredClients()
        //{
        //    if (!_signInManager.IsSignedIn(User))
        //    {
        //        return Redirect("~/Account/LoginAdmin");
        //    }
        //    var model = _clientRepository.Find(x => x.ApplicationUserID != null);
        //    return View(model);
        //}

        //[Route("Client/Add")]
        //[HttpGet]
        //public IActionResult AddClient()
        //{
        //    AddClientViewModel model = new AddClientViewModel();
        //    if (model.businessstreams.Count == 0)
        //    {
        //        model.businessstreams = new List<SelectListItem>();
        //        model.businessstreams.Add(new SelectListItem
        //        {
        //            Value = "",
        //            Text = "-- Choose Industry --"
        //        });

        //        var businessstreams = _businessRepository.GetAll();

        //        foreach (var BusinessStrem in businessstreams)
        //        {
        //            model.businessstreams.Add(new SelectListItem
        //            {
        //                Value = Convert.ToString(BusinessStrem.Id),
        //                Text = BusinessStrem.Name
        //            });
        //        }

        //    }
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
        //                Text = province.Name
        //            });
        //        }
        //    }

        //    return View(model);
        //}

        //[Route("Client/Add")]
        //[HttpPost]
        //public IActionResult AddClient(AddClientViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var postaladdress = model.PostalAddrss1 + "-" + model.PostalAddrss2;
        //        PostalCode postalcode = _postalCodeRepository.GetPostalCodeDetail(postaladdress);
        //        var client = new Client { CompanyName = model.CompanyName, ContactEmail = model.ContactEmail, ContactPerson = model.ContactPerson, ContactNumber = model.ContactNumber, WebsiteUrl = model.WebsiteUrl, businessstreamID = model.businessstreamID, Address = model.Address, AboutCompany = model.AboutCompany, PostalCode = postalcode };

        //        int result = _clientRepository.Create(client);
        //        if (result == 1)
        //        {
        //            return Redirect("~/Admin");
        //        }
        //        else
        //        {
        //            ViewBag.ErrorMsg = "Error in saving data";
        //            return View(model);
        //        }

        //    }
        //    else
        //    {
        //        ViewBag.ErrorMsg = "Validation Failed";
        //        return View(model);
        //    }

        //}

        //[Route("Client/Edit/{id:int}")]
        //[HttpGet]
        //public IActionResult EditClient(int id)
        //{
        //    Client client = _clientRepository.GetById(id);
        //    AddClientViewModel model = new AddClientViewModel();
        //    model.Id = id;
        //    model.CompanyName = client.CompanyName;
        //    model.WebsiteUrl = client.WebsiteUrl;
        //    model.ContactEmail = client.ContactEmail;
        //    model.ContactPerson = client.ContactPerson;
        //    model.ContactNumber = client.ContactNumber;
        //    model.businessstreamID = client.businessstreamID;
        //    model.Address = client.Address;
        //    model.AboutCompany = client.AboutCompany;

        //    model.businessstreams = new List<SelectListItem>();
        //    model.businessstreams.Add(new SelectListItem
        //    {
        //        Value = "",
        //        Text = "-- Choose Industry --"
        //    });

        //    var businessstreams = _businessRepository.GetAll();

        //    foreach (var BusinessStrem in businessstreams)
        //    {
        //        model.businessstreams.Add(new SelectListItem
        //        {
        //            Value = Convert.ToString(BusinessStrem.Id),
        //            Text = BusinessStrem.Name
        //        });
        //    }
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
        //                Text = province.Name
        //            });
        //        }
        //    }
        //    model.businessstreamID = client.businessstreamID;
        //    //string postalcode = model.PostalAddrss1 + "-" + model.PostalAddrss2;
        //    PostalCode postalcodeobj = _postalCodeRepository.GetById(client.PostalCodeID);
        //    string postalarea = postalcodeobj.Code.Split('-')[0];
        //    string postalcode = postalcodeobj.Code.Split('-')[1];
        //    model.PostalAddrss1 = postalarea;
        //    model.PostalAddrss2 = postalcode;
        //    model.CityName = postalcodeobj.CityName;
        //    model.Town = postalcodeobj.Town;
        //    model.ProvinceID = client.PostalCode.ProvinceID;
        //    return View(model);
        //}

        //[Route("Client/Edit")]
        //[HttpPost]
        //public IActionResult EditClient(AddClientViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var postaladdress = model.PostalAddrss1 + "-" + model.PostalAddrss2;
        //        PostalCode postalcode = _postalCodeRepository.GetPostalCodeDetail(postaladdress);
        //        var client = new Client { Id = model.Id, CompanyName = model.CompanyName, ContactEmail = model.ContactEmail, ContactPerson = model.ContactPerson, ContactNumber = model.ContactNumber, WebsiteUrl = model.WebsiteUrl, businessstreamID = model.businessstreamID, Address = model.Address, AboutCompany = model.AboutCompany, PostalCode = postalcode };

        //        int result = _clientRepository.Update(client);
        //        if (result == 1)
        //        {
        //            return Redirect("~/Admin");
        //        }
        //        else
        //        {
        //            ViewBag.ErrorMsg = "Error in editing data";
        //            return View(model);
        //        }
        //    }
        //    else
        //    {
        //        ViewBag.ErrorMsg = "Validation Failed";
        //        return View(model);
        //    }

        //}

        //[Route("Client/Delete/{id:int}")]
        //[HttpPost]
        //public IActionResult DeleteClient(int id)
        //{
        //    Client client = new Client()
        //    {
        //        Id = id
        //    };
        //    _clientRepository.Delete(client);

        //    //AddClientViewModel model = new AddClientViewModel();
        //    //if (model.businessstreams.Count == 0)
        //    //{
        //    //    model.businessstreams = new List<SelectListItem>();
        //    //    model.businessstreams.Add(new SelectListItem
        //    //    {
        //    //        Value = "1",
        //    //        Text = "All"
        //    //    });

        //    //}

        //    return Redirect("~/Admin");
        //}

        //[Route("Client/Detail/{id:int}")]
        //[HttpGet]
        //public IActionResult DetailClient(int id)
        //{
        //    var model = _clientRepository.GetById(id);
        //    return View(model);
        //}

        //[Route("Client/AutoRegister/{id:int}")]
        //[HttpPost]
        //public IActionResult AutoRegister(int id)
        //{
        //    Guid guid = Guid.NewGuid();
        //    var companyInfo = _clientRepository.GetById(id);
        //    string username = guid.ToString().Split('-')[0];
        //    string password = guid.ToString().Split('-')[1] + DateTime.Now.Date.ToString("yyyyMdd");

        //    var appUser = new ApplicationUser { UserName = username, EmailConfirmed = true };

        //    if (!_roleManager.RoleExistsAsync("client").Result)
        //    {
        //        var create = _roleManager.CreateAsync(new IdentityRole("client")).Result;
        //    }
        //    else
        //    {
        //        var result = _userManager.CreateAsync(appUser, password).Result;
        //        if (result.Succeeded)
        //        {
        //            var roleRegister = _userManager.AddToRoleAsync(appUser, "client").Result;
        //            if (roleRegister.Succeeded)
        //            {
        //                companyInfo.ApplicationUser = appUser;
        //                _clientRepository.Update(companyInfo);
        //                string[] companyPersons = companyInfo.ContactPerson.Split(",");
        //                string[] companyEmails = companyInfo.ContactEmail.Split(",");
        //                List<EmailAddress> emailList = new List<EmailAddress>();
        //                for (int i = 0; i < companyEmails.Length; i++)
        //                {
        //                    emailList.Add(new EmailAddress { Name = companyPersons[i].Trim(), Address = companyEmails[i].Trim() });
        //                }

        //                dynamic emails =
        //          JsonConvert.DeserializeObject(System.IO.File.ReadAllText("Formats/emails.json"));
        //                var emailformat = emails["clientcredential"];
        //                string contentBody = emailformat["content"].Value;
        //                //replacing for username and passsword
        //                contentBody = contentBody.Replace("$username$", username);
        //                contentBody = contentBody.Replace("$password$", password);

        //                EmailMessage emailMessage = new EmailMessage()
        //                {
        //                    ToAddresses = emailList,
        //                    Subject = emailformat["subject"].Value,
        //                    Content = contentBody
        //                };
        //                //send mail by using no-reply-notification@mtic.co.jp
        //                try
        //                {
        //                    _emailService.Send(emailMessage);
        //                    ViewData["message"] = "Credetials sent to the client successfully";
        //                }
        //                catch(Exception ex)
        //                {
        //                    List<EmailAddress> errEmailList = new List<EmailAddress>();
        //                    errEmailList.Add(new EmailAddress()
        //                    {
        //                        Name = "Dhananjay",
        //                        Address = "dhananjay.singh@mtic.co.jp"
        //                    });
        //                    _emailService.SendEmailAsync("dhananjay.singh@mtic.co.jp","Error in Sending Mail", ex.Message);
        //                    ViewData["message"] = "Error in sending mail";
        //                    return Content("1");
        //                }

        //                //return Redirect("~/Admin/Client/Success");
        //                return Content("0");
        //            }
        //        }
        //    }
        //    ViewData["message"] = "Error in saving data";
        //    return Content("1");
        //    //return View();

        //}

        //[Route("Client/Success")]
        //public IActionResult Success()
        //{
        //    return View();
        //}

        //[Route("User/Register")]
        //[HttpGet]
        ////[Authorize(Roles = "admin")]
        //[AllowAnonymous]
        //public IActionResult RegisterUser()
        //{
        //    return View();
        //}

        //[Route("User/Register")]
        //[HttpPost]
        //[AllowAnonymous]
        //public async Task<IActionResult> RegisterUser(RegisterUserViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = await _userManager.FindByEmailAsync(model.Email);
        //        //check if user already exists or not
        //        if (user == null)
        //        {
        //            user = new ApplicationUser { UserName = model.Email, Email = model.Email, PhoneNumber = model.PhoneNumber, EmailConfirmed = true };
        //            var result = await _userManager.CreateAsync(user, model.Password);
        //            if (!result.Succeeded)
        //            {
        //                ViewData["message"] = "Error in saving data";

        //                return View(model);
        //            }
        //        }
        //        //check user already registered as admin or not
        //        if (await _userManager.IsInRoleAsync(user, "admin"))
        //        {
        //            ViewData["message"] = "User already registered as admin";
        //            ModelState.Clear();
        //            return View();
        //        }

        //        string rolename = model.RoleName;
        //        if (!await _roleManager.RoleExistsAsync(rolename))
        //        {
        //            var create = await _roleManager.CreateAsync(new IdentityRole(rolename));
        //            if (create.Succeeded)
        //            {
        //                //_logger.LogInformation("User Role is created ");
        //            }
        //        }

        //        var roleRegister = await _userManager.AddToRoleAsync(user, rolename);
        //        if (roleRegister.Succeeded)
        //        {
        //            ViewData["message"] = "Successfully Save Record";

        //            //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        //            //var callbackUrl = Url.EmailConfirmationLink(user.Id, code, Request.Scheme);
        //            //Email sending code
        //            //EmailAddress toEmailAddress = new EmailAddress();
        //            //toEmailAddress.Name = model.Email;
        //            //toEmailAddress.Address = model.Email;
        //            //EmailMessage emailMessage = new EmailMessage()
        //            //{
        //            //    ToAddresses = new List<EmailAddress>() {
        //            //toEmailAddress
        //            //},
        //            //Subject = "Confirm your email",
        //            // Content = $"Congratulation! you have successfully registerd with Ajj, Please click on link confirmation : <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>{HtmlEncoder.Default.Encode(callbackUrl).ToString()}</a>"
        //            //};
        //            // _emailService.Send(emailMessage);
        //            return View();

        //        }

        //    }

        //    return View(model);
        //}

        //[Route("User/ManageRole")]
        //[HttpGet]
        ////[Authorize(Roles = "admin")]
        //[AllowAnonymous]
        //public IActionResult ManageRole()
        //{
        //    return View();
        //}

        //[Route("User/ManageRole")]
        //[HttpPost]
        ////[Authorize(Roles = "admin")]
        //[AllowAnonymous]
        //public IActionResult ManageRole(string UserName, string RoleName)
        //{
        //    return View();
        //}
    }
}