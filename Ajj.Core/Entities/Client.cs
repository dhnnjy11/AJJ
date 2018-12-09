using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ajj.Core.Entities
{
    /// <summary>
    /// It has all details of company
    /// </summary>
    [Table("clients")]
    public class Client
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public int? BusinessstreamID { get; set; } //make it nullable using ? 
        public BusinessStream Businessstream { get; set; }
        public string WebsiteUrl { get; set; }
        public string Address { get; set; }
        public string ContactPerson { get; set; }
        public string ContactEmail { get; set; }
        public string ContactNumber { get; set; }
        public string AboutCompany { get; set; }
        public bool IsActive { get; set; } = true;
        public char Status { get; set; } //A = Active , I = Inactive 
        public DateTime UpdateDate { get; set; } = DateTime.Now;

        //link with application user
        public string ApplicationUserID { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        //link with postal code id 
        public int PostalCodeID { get; set; }
        public PostalCode PostalCode { get;set;}

        public List<CompanyImage> CompanyImages { get; set; }
        public List<CompanyUser> CompanyUsers { get; set; }




    }
}
