using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ajj.Models.HomeViewModel
{
    public class ContactViewModel
    {
        public string CompanyName { get; set; }
        public string PersonIncharge { get; set; }
        public string Phone { get; set; }

        [Required]
        [Display(Name = "EmailAddress")]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [Required]
        [Display(Name = "EmailAddress")]
        [Compare("EmailAddress", ErrorMessage = "The Email and confirmation Email do not match.")]
        [EmailAddress]
        public string ConfirmEmail { get; set; }

        public string ContentSection { get; set; }
        public string InformationRequest { get; set; }
        public string Message { get; set; }
    }
}
