using System;
using System.Collections.Generic;
using System.Text;

namespace Ajj.Core.Entities
{
    public class Address
    {
        public string Prefereture { get; set; }
        public string CityName { get; set; }
        public string Town { get; set; }
        public string ZipCode { get; set; }
        public string StreetAddress { get; set; }

        public Address() { }
        public Address(string Prefereture,string CityName,string Town, string ZipCode, string StreetAddress)
        {
            this.StreetAddress = StreetAddress;
            this.Town = Town; 


        }
    }
}
