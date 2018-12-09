using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Ajj.Models
{
    [Table("postalcodes")]
    public class PostalCode
    {
        [Key]
        public int Id { get; set; }
        public string Code { get; set; }
        public string ProvinceName { get; set; }
        public string CityName { get; set; }
        public string Town { get; set; }
        public string CityName_En { get; set; }
        public string Town_En { get; set; }

        public int ProvinceID { get; set; }
        public Province Province { get; set; }
    }
}
