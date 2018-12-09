using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Ajj.Models
{
    
    public class JobSeeker 
    {
        public int Id { get; set; }

        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        [Column("First_Name")]
        public string FirstName { get; set; }
        [Column("Last_Name")]
        public string LastName { get; set; }
        //public string DateofBirth { get; set; }
        [Column("Birth_Year")]
        public string BirthYear { get; set; }
        [Column("Birth_Month")]
        public string BirthMonth { get; set; }
        [Column("Birth_Day")]
        public string BirthDay { get; set; }
        [Column("Birth_Age")]
        public string Age { get; set; }
        [Column("Radio_Sex")]
        public char Gender { get; set; }
        //public string Nationality { get; set; }
        public string Visa { set; get; }
        public string SubVisaType { get; set; }
        public string OtherCountry { get; set; }
        public char InJapan { set; get; }
        public string Address { set; get; }
        //public string Povince { set; get; }
        public string City { set; get; }
        public string Town { get; set; }
        public string PostalAddrss { get; set; }
        public List<JobApply> JobsApply { get; set; }

        public int ProvinceID { get; set; }
        public Province Province { get; set; }

        public int CountryID { get; set; }
        public Country Country { get; set; }
    }
}
