using Ajj.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ajj.Areas.Admin.Models
{
    public class AddClientViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public string WebsiteUrl { get; set; }
        public int? BusinessstreamID { get; set; }
        public BusinessStream businessstream { get; set; }
        public string ContactNumber { get; set; }
        public string ContactPerson { get; set; }
        public string ContactEmail { get; set; }
        public string PostalAddrss1 { get; set; }
        public string PostalAddrss2 { get; set; }
        public int? ProvinceID { get; set; }
        [Required(ErrorMessage = "Postal Code is either incorrect or missing")]
        public string CityName { get; set; }
        public string Town { get; set; }
        public string Address { set; get; }
        public string AboutCompany { get; set; }
        public List<IFormFile> Files { get; set; }
        public IEnumerable<SelectListItem> Provinces { get; set; }
        public IEnumerable<SelectListItem> businessstreams { get; set; }
    }
}