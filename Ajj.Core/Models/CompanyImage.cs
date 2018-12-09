using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ajj.Models
{
    public class CompanyImage
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; }
    }
}
