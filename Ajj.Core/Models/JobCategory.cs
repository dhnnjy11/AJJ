﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Ajj.Models
{
    [Table("jobcategories")]
    public class JobCategory
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }

        public BusinessStream businessstream { get; set; }
        public int BusinessStreamId { get; set; }
    }
}
