using System.ComponentModel.DataAnnotations.Schema;

namespace Ajj.Models
{
    /// <summary>
    /// It has all details of company
    /// </summary>
    [Table("clients")]
    public class Client
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        //public string ProfileDesc { get; set; }
        public int? BusinessstreamID { get; set; } //make it nullable using ? 
        public BusinessStream Businessstream { get; set; }
        //public string EstablishedDate { get; set; }
        public string WebsiteUrl { get; set; }
        public string Address { get; set; }
        //public string City { get; set; }
        //public string PostalAddrss { get; set; }
        public string ContactPerson { get; set; }
        public string ContactEmail { get; set; }
        public string ContactNumber { get; set; }
        //public string CompanyImageUrl { get; set; }
        public string AboutCompany { get; set; }
        //public int ProvinceID { get; set; }
        //public Province Province { get; set; }
        public string ApplicationUserID { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public int PostalCodeID { get; set; }
        public PostalCode PostalCode { get;set;}

    }
}
