using System;
using System.Collections.Generic;
using System.Text;

namespace Ajj.Core.Entities
{
    public class CompanyUser 
    {

        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public int ClientId { get; set; }
        public Client Client { get; set; }
    }
}
