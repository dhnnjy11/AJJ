﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ajj.Areas.Clients.Models
{
    public class ChangeUserNameViewModel
    {
        [Required]
        public string UserName { get; set; }
    }
}
