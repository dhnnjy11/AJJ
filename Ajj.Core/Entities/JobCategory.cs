using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Ajj.Core.Entities
{
    [Table("jobcategories")]
    public class JobCategory
    {        
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string CategoryName_JP { get; set; }

        public BusinessStream businessstream { get; set; }
        public int BusinessStreamId { get; set; }
    }
}
