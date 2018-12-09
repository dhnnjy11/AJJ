using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ajj.Interface
{
    public interface IAutoFillService
    {
        Task<IEnumerable<SelectListItem>> AddCountryListAsync();
        Task<IEnumerable<SelectListItem>> AddProvinceListAsync();
        IEnumerable<SelectListItem> AddVisaList();
        int CalculateYourAge(DateTime Dob);
    }
}
