using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ajj.Models.AccountViewModels
{
    public class RegisterClientViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        public string CompanyName { get; set; }

        [Required]
        public string WebsiteUrl { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string CompanyEmail { get; set; }
        public string UserName { get; set; }
     
        public string PostalAddrss1 { get; set; }
        public string PostalAddrss2 { get; set; }

        [Required]
        [StringLength(15, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 10)]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }        
        public string ContactPerson { get; set; }
        public int ProvinceID { get; set; }
        [Required(ErrorMessage ="Postal Code is not valid")]
        public string CityName { set; get; }        
        public string Town { get; set; }
        public string Address { set; get; }
        public int BusinessstreamID { get; set; }
        public List<IFormFile> Files { get; set; }
        public IEnumerable<SelectListItem> Provinces { get; set; }
        public IEnumerable<SelectListItem> businessstreams { get; set; }

    }
}
