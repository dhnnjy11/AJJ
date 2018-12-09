using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ajj.Areas.Admin.Models
{
    public class UserRoleViewModel
    {
        public int UserID { get; set; }
        public int RoleID { get; set; }

        public List<SelectListItem> Roles { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> Users { get; set; } = new List<SelectListItem>();
    }
}
