using Ajj.Attributes;
using Ajj.Core.Entities;
using Ajj.Core.Interface;
using Ajj.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ajj.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/AjjSyncUsers")]
    [BasicAuthorization("")]
    public class AjjSyncUsersController : ControllerBase
    {
        private readonly IJobSeekerRepository _jobSeeekerRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICountryRepository _countryRepository;
        private readonly IProvinceRepository _provinceRepository;
        private readonly IJobSeekerService _jobSeekerService;
        private readonly IEmailService _emailService;

        public AjjSyncUsersController(UserManager<ApplicationUser> userManager,
            IJobSeekerRepository jobSeekerRepository,
            ICountryRepository countryRepository,
            IProvinceRepository provinceRepository,
            IJobSeekerService jobSeekerService,
            IEmailService emailService
            )
        {
            _userManager = userManager;
            _jobSeeekerRepository = jobSeekerRepository;
            _countryRepository = countryRepository;
            _provinceRepository = provinceRepository;
            _jobSeekerService = jobSeekerService;
            _emailService = emailService;
        }

        [HttpGet(Name = "Get")]
        public async Task<IActionResult> GetAsync(string email)
        {
            try
            {
                ApplicationUser user = await _userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    return NotFound(new { result = "ERROR1", error = "user not found" });
                }
                var jobseekers = await _jobSeeekerRepository.FindByAsyn(x => x.ApplicationUserId == user.Id);

                GBUser model = new GBUser();
                foreach (var jobseeker in jobseekers)
                {
                    var country = _countryRepository.GetById(jobseeker.CountryID);
                    var province = _provinceRepository.GetById(jobseeker.ProvinceID);

                    if (country != null)
                    {
                        model.country = country.Name;
                    }
                    if (province != null)
                    {
                        model.thestate = province.Name_Jp;
                    }

                    model.first_name = jobseeker.FirstName;
                    model.last_name = jobseeker.LastName;
                    model.birth_year = jobseeker.BirthYear;
                    model.birth_month = jobseeker.BirthMonth;
                    model.birth_day = jobseeker.BirthDay;
                    model.zip = jobseeker.PostalAddrss;
                    model.visa = jobseeker.Visa;
                    model.addr1 = jobseeker.Address;
                    model.birth_age = jobseeker.Age;
                    model.thestate_other = jobseeker.OtherCountry;

                    if (jobseeker.Gender == 'M')
                    {
                        model.radio_sex = "Male";
                    }
                    else if (jobseeker.Gender == 'F')
                    {
                        model.radio_sex = "Female";
                    }

                    model.city = jobseeker.City;

                    model.visa = jobseeker.Visa;
                    //model.vopen1 = jobseeker.SubVisaType;
                    model.user_email = user.Email;
                    model.user_pass = user.PasswordHash;
                    model.mobile = user.PhoneNumber;
                }
                return Ok(new { result = "OK", userinfo = model });
            }
            catch (Exception ex)
            {
                return CreatedAtAction("Get", new { result = "ERROR2", error = ex.Message });
            }
        }

        [HttpPost("CreateUser", Name = "Post")]
        public async Task<IActionResult> PostAsync([FromBody]GBUser gbUser)
        {
            Dictionary<string, string> returnMsg = new Dictionary<string, string>();
            try
            {
                JobSeeker jobseeker = new JobSeeker();
                if (ModelState.IsValid)
                {
                    await SaveGBUserToAJJAsync(gbUser, jobseeker);

                    ApplicationUser appuser = new ApplicationUser()
                    {
                        UserName = gbUser.user_email,
                        PasswordHash = gbUser.user_pass,
                        PhoneNumber = gbUser.mobile,
                        Email = gbUser.user_email,
                        CreateDate = DateTime.Now,
                        EmailConfirmed = true
                    };

                    var result = _userManager.CreateAsync(appuser).Result;
                    if (result.Succeeded)
                    {
                        var roleRegister = _userManager.AddToRoleAsync(appuser, "candidate").Result;
                        jobseeker.ApplicationUser = appuser;
                        _jobSeeekerRepository.Create(jobseeker);
                        return Ok(new { result = "OK", error = "" });
                    }
                    else
                    {
                        string err = "";
                        foreach (var error in result.Errors)
                        {
                            err += error.Description + ",";
                        }
                        await SendApiErrorReportAsync(gbUser.user_email, "CreateUser", "dhananjay.singh@mtic.co.jp", err);
                        return BadRequest(new { result = "ERROR2", error = err });
                    }
                }
                else
                {
                    string err = "";

                    foreach (var child in ModelState.Root.Children)
                    {
                        foreach (var error in child.Errors)
                        {
                            err += error.ErrorMessage;
                        }
                    }
                    await SendApiErrorReportAsync(gbUser.user_email, "CreateUser", "dhananjay.singh@mtic.co.jp", err);
                    return BadRequest(new { result = "ERROR1", error = err });
                }
            }
            catch (Exception ex)
            {
                await SendApiErrorReportAsync(gbUser.user_email, "CreateUser", "dhananjay.singh@mtic.co.jp", ex.Message);
                return StatusCode(500, new { result = "ERROR2", error = ex.Message });
            }
        }

        [HttpPut(Name = "ChangePassword"), Route("[action]")]
        public async Task<IActionResult> ChangePasswordAsync([FromBody]ChangePassword model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ApplicationUser user = await _userManager.FindByEmailAsync(model.Email);
                    if (user != null)
                    {
                        var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);

                        if (result.Succeeded)
                        {
                            return Ok(new { result = "OK", error = "" });
                        }
                        else
                        {
                            return BadRequest(new { result = "ERROR2", error = "Failed to update" });
                        }
                    }
                    else
                    {
                        return BadRequest(new { result = "ERROR2", error = "User Not Found" });
                    }
                }
                else
                {
                    string err = "";

                    foreach (var child in ModelState.Root.Children)
                    {
                        foreach (var error in child.Errors)
                        {
                            err += error.ErrorMessage;
                        }
                    }
                    return BadRequest(new { result = "ERROR1", error = err });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { result = "ERROR2", error = ex.Message });
            }
        }

        [HttpPut(Name = "ResetPassword"), Route("[action]")]
        public async Task<IActionResult> ResetPasswordAsync([FromForm]string email, string password)
        {
            try
            {

                ApplicationUser user = await _userManager.FindByEmailAsync(email);
                if (user != null)
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var result = await _userManager.ResetPasswordAsync(user, token, password);

                    if (result.Succeeded)
                    {
                        return Ok(new { result = "OK", error = "" });
                    }
                    else
                    {
                        return BadRequest(new { result = "ERROR2", error = "Failed to update" });
                    }
                }
                else
                {
                    return BadRequest(new { result = "ERROR2", error = "User Not Found" });
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    result = "ERROR2",
                    error = ex.Message
                });
            }
        }

        [HttpPut(Name = "UpdateUserInfo"), Route("[action]")]
        public async Task<IActionResult> UpdateUserInfoAsync([FromBody]GBUser gbUser)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    ApplicationUser user = await _userManager.FindByEmailAsync(gbUser.user_email);
                    var jobseeker = _jobSeeekerRepository.GetJobSeekerByUserId(user.Id);
                    await SaveGBUserToAJJAsync(gbUser, jobseeker);

                    ApplicationUser appuser = new ApplicationUser()
                    {
                        PhoneNumber = gbUser.mobile,
                        Email = gbUser.user_email,
                        PasswordHash = gbUser.user_pass
                    };

                    //jobseeker.ApplicationUser = appuser;
                    //var result = _userManager.UpdateAsync(user);

                    //if (result.IsCompletedSuccessfully)
                    //{
                    _jobSeeekerRepository.Update(jobseeker);
                    return Ok(new { result = "OK", error = "" });
                    // }
                    //else
                    //{
                    //return BadRequest(new { result = "ERROR1", error = "User info failed to update" });
                    // }


                }
                else
                {
                    string err = "";

                    foreach (var child in ModelState.Root.Children)
                    {
                        foreach (var error in child.Errors)
                        {
                            err += error.ErrorMessage;
                        }
                    }
                    return BadRequest(new { result = "ERROR1", error = err });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { result = "ERROR2", error = ex.Message });
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        private async Task SaveGBUserToAJJAsync(GBUser gbUser, JobSeeker jobseeker)
        {
            if (!String.IsNullOrEmpty(gbUser.country))
            {
                var findCountry = await _countryRepository.FindByAsyn(x => x.Name == gbUser.country);

                foreach (var nationality in findCountry)
                {
                    jobseeker.Country = nationality;
                }
            }

            if (jobseeker.Country == null)
            {
                jobseeker.Country = new Country();
            }

            if (!String.IsNullOrEmpty(gbUser.thestate))
            {
                var findCountry = await _provinceRepository.FindByAsyn(x => x.Name_Jp == gbUser.thestate);
                foreach (var prefecture in findCountry)
                {
                    jobseeker.Province = prefecture;
                }
            }

            if (jobseeker.Province == null)
            {
                jobseeker.Province = new Province();
            }

            jobseeker.FirstName = gbUser.first_name;
            jobseeker.LastName = gbUser.last_name;
            jobseeker.BirthYear = gbUser.birth_year;
            jobseeker.BirthMonth = gbUser.birth_month;
            jobseeker.BirthDay = gbUser.birth_day;
            jobseeker.OtherCountry = gbUser.thestate_other;
            jobseeker.Origin = "GB";
            if (!String.IsNullOrEmpty(gbUser.radio_sex))
            {
                if (gbUser.radio_sex.ToLower().Trim() == "male")
                {
                    jobseeker.Gender = 'M';
                }
                else if (gbUser.radio_sex.ToLower().Trim() == "female")
                {
                    jobseeker.Gender = 'F';
                }
            }

            jobseeker.PostalAddrss = gbUser.zip;
            jobseeker.Address = gbUser.addr1;
            jobseeker.Age = gbUser.birth_age;
            jobseeker.Visa = gbUser.visa;
            string visacat = gbUser.visa;
            string subvisa = "";


            if (!String.IsNullOrEmpty(gbUser.vopen1))
            {
                subvisa = gbUser.vopen1;
                jobseeker.SubVisaType = gbUser.vopen1;
            }
            else if (!String.IsNullOrEmpty(gbUser.vopen2))
            {
                if (gbUser.vopen2 == "No")
                {
                    jobseeker.IsPermitToWork = false;
                }
                else
                {
                    jobseeker.IsPermitToWork = true;
                }
            }
            else if (!String.IsNullOrEmpty(gbUser.vopen3))
            {
                if (gbUser.vopen3 == "No")
                {
                    jobseeker.IsPermitToWork = false;
                }
                else
                {
                    jobseeker.IsPermitToWork = true;
                }
            }
            else if (!String.IsNullOrEmpty(gbUser.vopen4))
            {
                if (gbUser.vopen4 == "No")
                {
                    jobseeker.IsPermitToWork = false;
                }
                else
                {
                    jobseeker.IsPermitToWork = true;
                }
            }
            else if (!String.IsNullOrEmpty(gbUser.vopen5))
            {
                subvisa = gbUser.vopen5;
                jobseeker.SubVisaType = gbUser.vopen5;
            }
            else if (!String.IsNullOrEmpty(gbUser.vopen6))
            {
                subvisa = gbUser.vopen6;
                jobseeker.SubVisaType = gbUser.vopen6;
            }

            var visa = _jobSeekerService.GetVisa(visacat, subvisa);
            jobseeker.VisaCategory = visa;
            jobseeker.VisaExpiryYear = gbUser.visa_year;
            jobseeker.VisaExpiryMonth = gbUser.visa_month;
            jobseeker.VisaExpiryDay = gbUser.visa_day;


        }

        // POST: api/SyncUsers //for entering new gb user entry
        //[HttpPost]
        //public string Post([FromBody]IEnumerable<GBUserViewModel> gbUsers)
        //{
        //    string returnMsg = "";
        //    try
        //    {
        //        foreach (var users in gbUsers)
        //        {
        //            JobSeeker jobseeker = new JobSeeker();
        //            if (!String.IsNullOrEmpty(users.country))
        //            {
        //                var findCountry = _countryRepository.Find(x => x.Name == users.country);
        //                foreach (var nationality in findCountry)
        //                {
        //                    jobseeker.Country = nationality;
        //                }
        //            }

        //            if (jobseeker.Country == null)
        //            {
        //                jobseeker.Country = new Country();
        //            }
        //            if (!String.IsNullOrEmpty(users.thestate))
        //            {
        //                var findCountry = _provinceRepository.Find(x => x.Name_Jp == users.thestate);
        //                foreach (var prefecture in findCountry)
        //                {
        //                    jobseeker.Province = prefecture;
        //                }
        //            }

        //            if (jobseeker.Province == null)
        //            {
        //                jobseeker.Province = new Province();
        //            }
        //            //var country = _countryRepository.GetById(jobseeker.CountryID);
        //            jobseeker.FirstName = users.first_name;
        //            jobseeker.LastName = users.last_name;
        //            jobseeker.BirthYear = users.birth_year;
        //            jobseeker.BirthMonth = users.birth_month;
        //            jobseeker.BirthDay = users.birth_day;
        //            if (String.IsNullOrEmpty(users.radio_sex))
        //            {
        //                if (users.radio_sex.ToLower().Trim() == "male")
        //                {
        //                    jobseeker.Gender = 'M';
        //                }
        //                else if (users.radio_sex.ToLower().Trim() == "female")
        //                {
        //                    jobseeker.Gender = 'F';
        //                }
        //            }
        //            jobseeker.PostalAddrss = users.zip;
        //            jobseeker.Visa = users.visa;
        //            jobseeker.Address = users.addr1;
        //            jobseeker.Age = users.birth_age;

        //            if (!String.IsNullOrEmpty(users.vopen1))
        //            {
        //                jobseeker.SubVisaType = users.vopen1;
        //            }
        //            else if (!String.IsNullOrEmpty(users.vopen2))
        //            {
        //                jobseeker.SubVisaType = users.vopen2;
        //            }
        //            else if (!String.IsNullOrEmpty(users.vopen3))
        //            {
        //                jobseeker.SubVisaType = users.vopen3;
        //            }
        //            else if (!String.IsNullOrEmpty(users.vopen4))
        //            {
        //                jobseeker.SubVisaType = users.vopen4;
        //            }
        //            else if (!String.IsNullOrEmpty(users.vopen5))
        //            {
        //                jobseeker.SubVisaType = users.vopen5;
        //            }
        //            else if (!String.IsNullOrEmpty(users.vopen6))
        //            {
        //                jobseeker.SubVisaType = users.vopen6;
        //            }

        //            ApplicationUser appuser = new ApplicationUser()
        //            {
        //                UserName = users.user_email,
        //                PasswordHash = users.user_pass,
        //                PhoneNumber = users.mobile,
        //                Email = users.user_email,
        //                CreateDate = DateTime.Now,
        //                EmailConfirmed = true

        //            };

        //            var result = _userManager.CreateAsync(appuser).Result;
        //            if (result.Succeeded)
        //            {
        //                var roleRegister = _userManager.AddToRoleAsync(appuser, "candidate").Result;
        //                //_roleManager.CreateAsync(appuser)
        //                jobseeker.ApplicationUser = appuser;
        //                _jobSeeekerRepository.Create(jobseeker);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return ex.Message;
        //    }

        //    return returnMsg;

        //}

        // PUT: api/SyncUsers/5 //for editing new gb user entry

        //GET: api/SyncUsers //get all details of users that is stored in ajj
        //public async Task<IEnumerable<GBUserViewModel>> Get()
        //{
        //    List<GBUserViewModel> ajjUsers = new List<GBUserViewModel>();
        //    try
        //    {
        //        var jobseekers = await _jobSeeekerRepository.GetAllAsyn();
        //        foreach (var jobseeker in jobseekers)
        //        {
        //            ApplicationUser user = await _userManager.FindByIdAsync(jobseeker.ApplicationUserId);
        //            GBUserViewModel model = new GBUserViewModel();

        //            var country = _countryRepository.GetById(jobseeker.CountryID);
        //            var province = _provinceRepository.GetById(jobseeker.ProvinceID);

        //            if (country != null)
        //            {
        //                model.country = country.Name;
        //            }
        //            if (province != null)
        //            {
        //                model.thestate = province.Name_Jp;
        //            }

        //            model.first_name = jobseeker.FirstName;
        //            model.last_name = jobseeker.LastName;
        //            model.birth_year = jobseeker.BirthYear;
        //            model.birth_month = jobseeker.BirthMonth;
        //            model.birth_day = jobseeker.BirthDay;
        //            model.zip = jobseeker.PostalAddrss;
        //            model.visa = jobseeker.Visa;
        //            model.addr1 = jobseeker.Address;
        //            model.birth_age = jobseeker.Age;
        //            model.thestate_other = jobseeker.OtherCountry;

        //            if (jobseeker.Gender == 'M')
        //            {
        //                model.radio_sex = "Male";
        //            }
        //            else if (jobseeker.Gender == 'F')
        //            {
        //                model.radio_sex = "Female";
        //            }

        //            model.city = jobseeker.City;
        //            model.visa = jobseeker.Visa;
        //            model.vopen1 = jobseeker.SubVisaType;
        //            model.user_email = user.Email;
        //            model.user_pass = user.PasswordHash;
        //            model.mobile = user.PhoneNumber;
        //            ajjUsers.Add(model);
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //    return ajjUsers;
        //}

        // GET: api/SyncUsers/5 //give detail of particular user stored in ajj

        //if (!String.IsNullOrEmpty(gbUser.country))
        //{
        //    var findCountry = await _countryRepository.FindByAsyn(x => x.Name == gbUser.country);

        //    foreach (var nationality in findCountry)
        //    {
        //        jobseeker.Country = nationality;
        //    }
        //}

        //if (jobseeker.Country == null)
        //{
        //    jobseeker.Country = new Country();
        //}

        //if (!String.IsNullOrEmpty(gbUser.thestate))
        //{
        //    var findCountry = await _provinceRepository.FindByAsyn(x => x.Name_Jp == gbUser.thestate);
        //    foreach (var prefecture in findCountry)
        //    {
        //        jobseeker.Province = prefecture;
        //    }
        //}

        //if (jobseeker.Province == null)
        //{
        //    jobseeker.Province = new Province();
        //}

        //jobseeker.FirstName = gbUser.first_name;
        //jobseeker.LastName = gbUser.last_name;
        //jobseeker.BirthYear = gbUser.birth_year;
        //jobseeker.BirthMonth = gbUser.birth_month;
        //jobseeker.BirthDay = gbUser.birth_day;
        //jobseeker.OtherCountry = gbUser.thestate_other;
        //if (!String.IsNullOrEmpty(gbUser.radio_sex))
        //{
        //    if (gbUser.radio_sex.ToLower().Trim() == "male")
        //    {
        //        jobseeker.Gender = 'M';
        //    }
        //    else if (gbUser.radio_sex.ToLower().Trim() == "female")
        //    {
        //        jobseeker.Gender = 'F';
        //    }
        //}
        //jobseeker.PostalAddrss = gbUser.zip;
        //jobseeker.Visa = gbUser.visa;
        //jobseeker.Address = gbUser.addr1;
        //jobseeker.Age = gbUser.birth_age;
        //jobseeker.IsAgreed = true;
        //string visacat = gbUser.visa;
        //string subvisa = "";

        //if (!String.IsNullOrEmpty(gbUser.vopen1))
        //{
        //    subvisa = gbUser.vopen1;
        //}
        //else if (!String.IsNullOrEmpty(gbUser.vopen2))
        //{
        //    if (gbUser.vopen2 == "No")
        //    {
        //        jobseeker.IsPermitToWork = false;
        //    }
        //    else
        //    {
        //        jobseeker.IsPermitToWork = true;
        //    }
        //}
        //else if (!String.IsNullOrEmpty(gbUser.vopen3))
        //{
        //    if (gbUser.vopen3 == "No")
        //    {
        //        jobseeker.IsPermitToWork = false;
        //    }
        //    else
        //    {
        //        jobseeker.IsPermitToWork = true;
        //    }
        //}
        //else if (!String.IsNullOrEmpty(gbUser.vopen4))
        //{
        //    if (gbUser.vopen4 == "No")
        //    {
        //        jobseeker.IsPermitToWork = false;
        //    }
        //    else
        //    {
        //        jobseeker.IsPermitToWork = true;
        //    }
        //}
        //else if (!String.IsNullOrEmpty(gbUser.vopen5))
        //{
        //    subvisa = gbUser.vopen5;
        //}
        //else if (!String.IsNullOrEmpty(gbUser.vopen6))
        //{
        //    subvisa = gbUser.vopen6;
        //}

        //var visa = _jobSeekerService.GetVisa(visacat, subvisa);
        //jobseeker.VisaCategory = visa;
        //jobseeker.VisaExpiryYear = gbUser.visa_year;
        //jobseeker.VisaExpiryMonth = gbUser.visa_month;
        //jobseeker.VisaExpiryDay = gbUser.visa_day;

        private async Task SendApiErrorReportAsync(string useremail, string apiName, string email, string errorMsg)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<b>Error API Name</b> : " + apiName + "<br/>");
            sb.Append("<b>User Email</b> : " + useremail + "<br/>");
            sb.Append("<b>Error Message</b> : " + errorMsg + "<br/>");

            string body = sb.ToString();
            string subject = "Error in sync";
            await _emailService.SendEmailAsync(useremail, subject, body);
            
            

        }

    }
}