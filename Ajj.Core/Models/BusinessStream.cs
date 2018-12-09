using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Ajj.Models
{
    /// <summary>
    /// This has different Stream of Business 
    /// </summary>
    [Table("businessstream")]
    public class BusinessStream
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Name_jp { get; set; }
    }
}
