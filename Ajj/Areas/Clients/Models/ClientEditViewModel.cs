using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ajj.Areas.Clients.Models
{
    public class ClientEditViewModel
    {
        public int ClientID { get; set; }
        public string UserID { get; set; }
        public string UserName { get; set; }
        [Required]
        public string CompanyName { get; set; }

        public string WebsiteUrl { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ContactPerson { get; set; }
        
        public string PhoneNumber { get; set; }
        [Required]
        public string PostalAddrss1 { get; set; }
        [Required]
        public string PostalAddrss2 { get; set; }
        public int? BusinessStreamID { get; set; }
        public int ProvinceID { get; set; }
        [Required(ErrorMessage = "Postal code is not valid")]
        public string CityName { get; set; }
        public string Town { get; set; }
        public string Address { get; set; }
        //public List<string> CompanyImages { get; set; }
        public List<IFormFile> Files { get; set; } 
        public List<SelectListItem> BusinessStreams { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> Provinces { get; set; } 
    }
}