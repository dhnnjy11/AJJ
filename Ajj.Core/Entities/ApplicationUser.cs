using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace Ajj.Core.Entities
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    [Table("ajjusers")]
    public class ApplicationUser : IdentityUser
    {
        [Column("User_Email")]
        public override string Email { get => base.Email; set => base.Email = value; }
        [Column("Mobile")] //Primary Phone number
        public override string PhoneNumber { get; set; }
        public DateTime CreateDate { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int ProvinceId { get; set; }
        public string CityName { get; set; }
        public string Town { get; set; }
        //public string PostalCode { get; set; }
        public string StreetAddress { get; set; }

        //public int ClientId { get; set; }
        //public Client Client { get; set; }

        public List<CompanyUser> CompanyUsers { get; set; }
    }
}
