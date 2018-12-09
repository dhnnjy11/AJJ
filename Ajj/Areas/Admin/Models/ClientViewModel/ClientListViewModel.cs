using Ajj.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ajj.Areas.Admin.Models.ClientViewModel
{
    public class ClientListViewModel
    {
        public string CompanyName { get; set; }
        public string Website { get; set; }
        public string BusinessStreamID { get; set; }
        public BusinessStream BusinessStream { get; set; }
        //public string City { get; set; }
        //public string PostalAddrss1 { get; set; }
        //public string PostalAddrss2 { get; set; }
        public string PhoneNumber { get; set; }
        public string ContactPerson { get; set; }        
        public int ProvinceID { get; set; }
        //public string Town { get; set; }
        public string Address { set; get; }
        public List<SelectListItem> BusinessStreams { get; set; } = new List<SelectListItem>();
    }
}
