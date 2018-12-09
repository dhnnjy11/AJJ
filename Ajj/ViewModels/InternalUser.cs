using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Ajj.Models
{
    [Table("internaluser")]
    public class CompanyUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
    }
}
