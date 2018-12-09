using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ajj.Models.HomeViewModel
{
    public class SearchJobViewModel
    {
        public string Keyword { get; set; }
        public string Location { get; set; }
        public string BusinessStreamId { get; set; }
        public string ProvinceId { get; set; }

        public List<SelectListItem> businessstreams { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> Provinces { get; set; } = new List<SelectListItem>();
    }
}
