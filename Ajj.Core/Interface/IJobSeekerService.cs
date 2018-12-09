using Ajj.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ajj.Core.Interface
{
    public interface IJobSeekerService
    {
        VisaCategory GetVisa(string category, string subCategory);
    }
}
