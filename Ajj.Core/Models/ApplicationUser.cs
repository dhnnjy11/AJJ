using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Ajj.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    [Table("ajjusers")]
    public class ApplicationUser : IdentityUser
    {
        [Column("User_Email")]
        public override string Email { get => base.Email ; set => base.Email = value; }      
        [Column("Mobile")]
        public override string PhoneNumber { get; set; }
        public DateTime CreateDate {get;set;}
        
    }
}
