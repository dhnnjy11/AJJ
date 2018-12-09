using Ajj.Areas.Admin.Models;
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
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Ajj.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin")]
    [Authorize(Roles = "admin")]
    public class ClientController : Controller
    {
        private readonly IImportService _importService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IClientRepository _clientRepository;
        private readonly IEmailService _emailService;
        private readonly IBusinessStreamRepository _businessRepository;
        private readonly IProvinceRepository _provinceRepository;
        private readonly IPostalCodeRepository _postalCodeRepository;
        private readonly IJobRepository _jobsRepository;
        private readonly IJobCategoryRepository _jobCategoryRepository;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IRepository<CompanyImage> _companyImage;
        private readonly IRepository<CompanyUser> _companyUsersRepository;
        private readonly IAutoFillService _autoFillService;

        public ClientController(IImportService importService,
           UserManager<ApplicationUser> userManager,
           SignInManager<ApplicationUser> signInManager,
           RoleManager<IdentityRole> roleManager,
           IClientRepository clientRepository,
           IEmailService emailService,
           IBusinessStreamRepository businessRepository,
           IProvinceRepository provinceRepository,
           //IEmailSender emailSender
           IPostalCodeRepository postalCodeRepository,
           IJobRepository jobsRepository,
           IJobCategoryRepository jobCategoryRepository,
           IHostingEnvironment hostingEnvironment,
           IRepository<CompanyImage> companyImage,
           IRepository<CompanyUser> companyUsersRepository,
           IAutoFillService autoFillService
           )
        {
            _importService = importService;
            _jobsRepository = jobsRepository;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _clientRepository = clientRepository;
            _emailService = emailService;
            _businessRepository = businessRepository;
            _jobCategoryRepository = jobCategoryRepository;
            _hostingEnvironment = hostingEnvironment;
            _companyImage = companyImage;
            _companyUsersRepository = companyUsersRepository;
            _autoFillService = autoFillService;
            _provinceRepository = provinceRepository;
            _postalCodeRepository = postalCodeRepository;
        }

        [HttpGet]
        [Route("/admin")]
        [Route("/admin/Client")]
        [AllowAnonymous]
        public IActionResult Index()
        {
            if (!_signInManager.IsSignedIn(User))
            {
                return Redirect("~/Account/LoginAdmin");
            }
            else
            {
                System.Security.Claims.ClaimsPrincipal currentUser = User;
                var isAdmin = currentUser.IsInRole("admin");

                if (isAdmin)
                {
                    var model = _clientRepository.Find(x => x.Status == 'I' || x.Status == 'P' || x.Status == 'E');
                    return View(model);
                }
            }

            return Redirect("~/Account/LoginAdmin");
        }

        [HttpGet]
        [Route("Client/List")]
        public IActionResult List()
        {
            if (!_signInManager.IsSignedIn(User))
            {
                return Redirect("~/Account/LoginAdmin");
            }
            var model = _clientRepository.Find(x => x.Status == 'A');
            return View(model);
        }

        [Route("Client/Add")]
        [HttpGet]
        public async Task<IActionResult> AddClientAsync()
        {
            AddClientViewModel model = new AddClientViewModel();

            model.Provinces = await _autoFillService.AddProvinceListAsync();
            //if (model.businessstreams.Count == 0)
            //{
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
            //}
            //if (model.Provinces.Count == 0)
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

        [Route("Client/Add")]
        [HttpPost]
        public async Task<IActionResult> AddClientAsync(AddClientViewModel model)
        {
            model.Provinces = await _autoFillService.AddProvinceListAsync();
            if (ModelState.IsValid)
            {
                var postaladdress = model.PostalAddrss1 + "-" + model.PostalAddrss2;
                PostalCode postalcode = _postalCodeRepository.GetPostalCodeDetail(postaladdress);

                var client = new Client { CompanyName = model.CompanyName, ContactEmail = model.ContactEmail ,ContactPerson = model.ContactPerson, ContactNumber = model.ContactNumber, WebsiteUrl = model.WebsiteUrl, BusinessstreamID = model.BusinessstreamID, Address = model.Address, AboutCompany = model.AboutCompany, PostalCode = postalcode, Status = 'I' };

                //Guid guid = Guid.NewGuid();
                string username = model.ContactEmail;
                //string password = guid.ToString().Split('-')[1] + DateTime.Now.Date.ToString("yyyyMdd");

                var appUser = new ApplicationUser { FirstName = model.FirstName, LastName = model.LastName, UserName = username, Email = model.ContactEmail, PhoneNumber = model.ContactNumber, CreateDate = DateTime.Now };

                if (!_roleManager.RoleExistsAsync("client").Result)
                {
                    await _roleManager.CreateAsync(new IdentityRole("client"));
                }

                var result = await _userManager.CreateAsync(appUser);
                if (result.Succeeded)
                {
                    //client.ApplicationUser = appUser;
                    var roleRegister = _userManager.AddToRoleAsync(appUser, "client").Result;
                }
                else
                {
                    AddErrors(result);
                    return View(model);
                }
                //_clientRepository.Create(client);
                await _clientRepository.AddAsyn(client);
                
                var companyclientmap = new CompanyUser
                {
                    Client = client,
                    ApplicationUser = appUser
                };
                await _clientRepository.SaveAsync();
                _companyUsersRepository.Create(companyclientmap);
                
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

                        CompanyImage companyImage = new CompanyImage();
                        companyImage.ImagePath = virtualFilePath;
                        companyImage.Client = client;
                        await _companyImage.AddAsyn(companyImage);
                        await _companyImage.SaveAsync();
                    }
                }

                return Redirect("~/Admin");
            }
            else
            {
                return View(model);
            }
        }

        [Route("Client/Edit/{id:int}")]
        [HttpGet]
        public async Task<IActionResult> EditClientAsync(int id)
        {
            Client client = _clientRepository.GetById(id);
            AddClientViewModel model = new AddClientViewModel();
            model.Id = id;
            model.CompanyName = client.CompanyName;
            model.WebsiteUrl = client.WebsiteUrl;
            model.ContactEmail = client.ContactEmail;
            model.ContactPerson = client.ContactPerson;
            model.ContactNumber = client.ContactNumber;
            model.BusinessstreamID = client.BusinessstreamID;
            model.Address = client.Address;
            model.AboutCompany = client.AboutCompany;

            //model.businessstreams = new List<SelectListItem>();
            //model.businessstreams.Add(new SelectListItem
            //{
            //    Value = "",
            //    Text = "-- Choose Industry --"
            //});

            //var businessstreams = _businessRepository.GetAll();

            //foreach (var BusinessStrem in businessstreams)
            //{
            //    model.businessstreams.Add(new SelectListItem
            //    {
            //        Value = Convert.ToString(BusinessStrem.Id),
            //        Text = BusinessStrem.Name
            //    });
            //}
            model.Provinces = await _autoFillService.AddProvinceListAsync();
            //if (model.Provinces.Count == 0)
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
            //model.BusinessstreamID = client.BusinessstreamID;
            //string postalcode = model.PostalAddrss1 + "-" + model.PostalAddrss2;
            PostalCode postalcodeobj = _postalCodeRepository.GetById(client.PostalCodeID);
            string postalarea = postalcodeobj.Code.Split('-')[0];
            string postalcode = postalcodeobj.Code.Split('-')[1];
            model.PostalAddrss1 = postalarea;
            model.PostalAddrss2 = postalcode;
            model.CityName = postalcodeobj.CityName;
            model.Town = postalcodeobj.Town;
            model.ProvinceID = client.PostalCode.ProvinceID;
            return View(model);
        }

        [Route("Client/Edit")]
        [HttpPost]
        public async Task<IActionResult> EditClientAsync(AddClientViewModel model)
        {
            model.Provinces = await _autoFillService.AddProvinceListAsync();
            if (ModelState.IsValid)
            {
                var postaladdress = model.PostalAddrss1 + "-" + model.PostalAddrss2;
                PostalCode postalcode = _postalCodeRepository.GetPostalCodeDetail(postaladdress);
                var client = _clientRepository.GetById(model.Id);
                client.CompanyName = model.CompanyName;
                client.ContactEmail = model.ContactEmail;
                client.ContactPerson = model.ContactPerson;
                client.ContactNumber = model.ContactNumber.Replace("-", "") ?? "";
                client.WebsiteUrl = model.WebsiteUrl;
                client.Address = model.Address;
                client.AboutCompany = model.AboutCompany;
                client.PostalCode = postalcode;

                //var clientuser = await _clientRepository.GetClientByUserId();
                //clientuser.UserName = model.ContactEmail;
                //clientuser.Email = model.ContactEmail;
                //clientuser.PhoneNumber = model.ContactNumber.Replace("-", "") ?? "";
                //var result = await _userManager.UpdateAsync(clientuser);
                //if (result.Succeeded)
                //{
                //    _clientRepository.Update(client);
                //}
                //else
                //{
                //    AddErrors(result);
                //    return View(model);
                //}
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

                        IEnumerable<CompanyImage> companyImages = _companyImage.Find(x => x.ClientId == model.Id);

                        if (companyImages.Count() < 1)
                        {
                            CompanyImage companyImage = new CompanyImage();
                            companyImage.ImagePath = virtualFilePath;
                            companyImage.Client = client;
                            await _companyImage.AddAsyn(companyImage);
                        }
                    }
                }


                return Redirect("~/Admin");
            }
            else
            {
                ViewBag.ErrorMsg = "Validation Failed";
                return View(model);
            }
        }

        [Route("Client/Delete/{id:int}")]
        [HttpPost]
        public async Task<IActionResult> DeleteClientAsync(int id)
        {
            var client = _clientRepository.GetById(id);
            //var user = await _userManager.FindByIdAsync(client.ApplicationUserID);
            _clientRepository.Delete(client);
            
            

            return Redirect("~/Admin");
        }

        [Route("Client/Detail/{id:int}")]
        [HttpGet]
        public IActionResult DetailClient(int id)
        {
            var model = _clientRepository.GetById(id);
            return View(model);
        }

        [Route("Client/AutoRegister/{id:int}")]
        [HttpPost]
        public async Task<IActionResult> AutoRegister(int id)
        {
            var companyInfo = _clientRepository.GetById(id);
            var appUser = await _userManager.FindByEmailAsync(companyInfo.ContactEmail); 

            var code = await _userManager.GeneratePasswordResetTokenAsync(appUser);
            var callbackUrl = Url.SetPasswordCallbackLink(appUser.Id, code, Request.Scheme);
            var domainName = HttpContext.Request.Host.Value;
            string[] companyPersons = companyInfo.ContactPerson.Split(",");
            string[] companyEmails = companyInfo.ContactEmail.Split(",");
            List<EmailAddress> emailList = new List<EmailAddress>();
            try
            {
                for (int i = 0; i < companyEmails.Length; i++)
                {
                    //emailList.Add(new EmailAddress { Name = companyPersons[i].Trim(), Address = companyEmails[i].Trim() });
                    _emailService.SendEmailRegisteredClientAsync(companyEmails[i], callbackUrl, domainName, companyInfo);
                    companyInfo.Status = 'P';//set company status as P = Pending (for setting password)
                    _clientRepository.Update(companyInfo);
                }
            }
            catch (Exception ex)
            {
                companyInfo.Status = 'E';//set company status as E = error in sending mail
                _clientRepository.Update(companyInfo);
                return Content("1");
            }

            return Content("0");
        }

        [Route("Client/Success")]
        public IActionResult Success()
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

            var model = new SetClientPasswordViewModel { Email = user.Email, Code = code };
            return View(model);
        }

        [Route("Client/SetPassword")]
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SetPasswordAsync(SetClientPasswordViewModel model)
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

        [Route("Client/AddErrors")]
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        [HttpPost]
        [Route("Client/Upload")]
        public async Task<IActionResult> UploadAsync(List<IFormFile> files)
        {
            IList<Client> clientList = new List<Client>();
            try
            {
                long size = files.Sum(f => f.Length);
                if (size > 0)
                {
                    foreach (var formFile in files)
                    {
                        Stream fileStream = formFile.OpenReadStream();
                        var clientData = await _importService.ImportExcelAsync(fileStream);

                        foreach (DataRow dr in clientData.Rows)
                        {
                            Client model = new Client();
                            int.TryParse(Convert.ToString(dr["Id"]), out int id);
                            if (id != 0)
                            {
                                model.Id = id;
                            }
                            model.CompanyName = Convert.ToString(dr["Company_Name"]);
                            model.WebsiteUrl = Convert.ToString(dr["Website_Url"]);
                            model.Address = Convert.ToString(dr["Address"]);
                            model.ContactEmail = Convert.ToString(dr["Contact_Email"]);
                            model.ContactPerson = Convert.ToString(dr["Contact_Person"]);
                            //model.AboutCompany = Convert.ToString(dr["About_Company"]);
                            model.ContactNumber = Convert.ToString(dr["Contact_Number"]);
                            var postalCode = _postalCodeRepository.GetPostalCodeDetail(Convert.ToString(dr["Postal_Code"] ?? 0));
                            model.PostalCode = postalCode;
                            if (dr["Postal_Code"] == null || dr["Postal_Code"].ToString().Trim() == "")
                            {
                                throw new Exception("Postal ID value can't be null");
                            }
                            model.Status = 'I'; //for initial status 
                            clientList.Add(model);
                        }
                        List<CompanyUser> companyusers = new List<CompanyUser>();
                        foreach (var client in clientList)
                        {
                            Guid guid = Guid.NewGuid();
                            string password = guid.ToString().Split('-')[1] + DateTime.Now.Date.ToString("yyyyMdd");
                            ApplicationUser user = new ApplicationUser()
                            {
                                UserName = client.ContactEmail,
                                PhoneNumber = client.ContactNumber != null ? client.ContactNumber.Replace("-", "") : client.ContactNumber,
                                CreateDate = DateTime.Now
                            };
                            var result = await _userManager.CreateAsync(user);
                            if (result.Succeeded)
                            {
                                _clientRepository.Add(client);
                                var companyUser = new CompanyUser
                                {
                                    Client = client,
                                    ApplicationUser = user
                                };
                                companyusers.Add(companyUser);
                                await _companyUsersRepository.AddAsyn(companyUser);
                            }

                        }
                       
                        await _clientRepository.AddListAsyn(clientList);
                        await _clientRepository.SaveAsync();
                        await _companyUsersRepository.SaveAsync();
                        //return Ok(new { message = "OK" });
                        TempData["success"] = "Successfully Uploaded";
                    }
                }
                else
                {
                    TempData["error"] = "No record found";
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                //return BadRequest(new { error = ex.Message });
            }
            //return StatusCode(500, new { error = "Something Wrong To upload file" });
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("Client/AddComment")]
        public IActionResult AddComment(string comment, int clientId)
        {
            var client = _clientRepository.GetById(clientId);
            client.AboutCompany = comment;
            _clientRepository.Update(client);
            return Ok("OK");

        }
    }
}