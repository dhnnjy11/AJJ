using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ajj.Areas.Clients.Models
{
    public class NewUserViewModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }        
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        public string PostalAddrss1 { get; set; }
        public string PostalAddrss2 { get; set; }
        public string ProvinceID { get; set; }
        public string CityName { get; set; }
        public string Town { get; set; }
        public string Address { get; set; }
        public IEnumerable<SelectListItem> Provinces { get; set; }
    }
}