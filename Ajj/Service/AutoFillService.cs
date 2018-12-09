using Ajj.Core.Entities;
using Ajj.Core.Interface;
using Ajj.Interface;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ajj.Service
{
    public class AutoFillService : IAutoFillService
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IProvinceRepository _provinceRepository;
        private readonly IRepository<VisaCategory> _visaCategoryRepository;

        public AutoFillService(ICountryRepository countryRepository,
            IProvinceRepository provinceRepository,
            IRepository<VisaCategory> visaCategoryRepository)
        {
            _countryRepository = countryRepository;
            _provinceRepository = provinceRepository;
            _visaCategoryRepository = visaCategoryRepository;
        }

        public async Task<IEnumerable<SelectListItem>> AddCountryListAsync()
        {
            //List<SelectListItem> countries = new List<SelectListItem>();
            //countries.Add(new SelectListItem
            //{
            //    Value = "",
            //    Text = "--Choose--"
            //});
            var countrieList = await _countryRepository.GetAllAsyn();
            var countries = countrieList.Select(x=> new SelectListItem
            {
                Value = Convert.ToString(x.Id),
                Text = x.Name
            });

            return countries;

        }

        public async Task<IEnumerable<SelectListItem>> AddProvinceListAsync()
        {          
            var provinceList = await _provinceRepository.GetAllAsyn();
            var provinces = provinceList.Select(x => new SelectListItem
            {
                Value = Convert.ToString(x.Id),
                Text = x.Name
            });
           

            return provinces;

        }

        public IEnumerable<SelectListItem> AddVisaList()
        {
            var visaList = _visaCategoryRepository.GetAll().Select(x =>
            new
            {
                x.ParentId,
                Name = x.Name.Trim()
            })
            .Distinct();
            var visacategories = visaList.Select(x => new SelectListItem
            {
                Value = Convert.ToString(x.ParentId),
                Text = x.Name
            });


            return visacategories;

        }

        public int CalculateYourAge(DateTime Dob)
        {
            DateTime now = DateTime.Now;
            int years = new DateTime(DateTime.Now.Subtract(Dob).Ticks).Year - 1;
            DateTime pastYearDate = Dob.AddYears(years);
            int months = 0;
            for (int i = 1; i <= 12; i++)
            {
                if (pastYearDate.AddMonths(i) == now)
                {
                    months = i;
                    break;
                }
                else if (pastYearDate.AddMonths(i) >= now)
                {
                    months = i - 1;
                    break;
                }
                
            }
            int days = now.Subtract(pastYearDate.AddMonths(months)).Days;
            int hours = now.Subtract(pastYearDate).Hours;
            int minutes = now.Subtract(pastYearDate).Minutes;
            int seconds = now.Subtract(pastYearDate).Seconds;
            return years;
        }
    }
}
