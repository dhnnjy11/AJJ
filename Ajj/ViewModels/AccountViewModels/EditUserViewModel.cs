using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ajj.Models.AccountViewModels
{
    public class EditUserViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BirthYear { get; set; }
        public string BirthMonth { get; set; }
        public string BirthDay { get; set; }
        public string Age { get; set; }
        public char Gender { get; set; }
        public string Nationality { get; set; }
        public int VisaTypeParentId { get; set; }
        public int VisaTypeId { get; set; }
        public string Visa { set; get; }
        public string SubVisaType { get; set; }
        public char InJapan { set; get; }
        public string Address { set; get; }
        public string Province { set; get; }
        public string City { set; get; }
        public string PostalAddrss1 { get; set; }
        public string PostalAddrss2 { get; set; }
        public string PhoneNumber { get; set; }
        public string OtherCountry { get; set; }
        public bool IsAgreed { get; set; }
        public bool IsPermitToWork { get; set; }
        public int ProvinceID { get; set; }
        public int CountryID { get; set; }
        public IEnumerable<SelectListItem> Countries { get; set; } 
        public IEnumerable<SelectListItem> Provinces { get; set; }
        public IEnumerable<SelectListItem> VisaTypes { get; set; }
        public string CityName { get; set; }
        public string Town { get; set; }
        public string PageType { get; set; }
        public string StatusMessage { get; set; }
    }
}