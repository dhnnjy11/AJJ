using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ajj.Areas.Clients.Models
{
    public class ClientRegisterViewModel
    {
        public int ClientID { get; set; }
        public string UserID { get; set; }
        [Required]
        public string CompanyName { get; set; }
        public string WebsiteUrl { get; set; }
        public string ContactPerson { get; set; }
        [Required]
        public string ContactEmail { get; set; }
        //public string BusinessStream { get; set; }
        [Required]
        public string PostalCode { get; set; }
        public string ProvinceName { get; set; }
        public string CityName { get; set; }
        public string Town { get; set; }
        public string Address { get; set; }
        

    }
}
