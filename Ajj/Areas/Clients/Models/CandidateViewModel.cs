using System;

namespace Ajj.Areas.Clients.Models
{
    public class CandidateViewModel
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string Address { get; set; }
        public DateTime AppliedDate { get; set; }
        public string ContactNumber { get; set; }
    }
}