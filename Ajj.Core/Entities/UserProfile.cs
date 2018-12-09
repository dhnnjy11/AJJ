namespace Ajj.Core.Entities
{
    public class UserProfile
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Prefereture { get; set; }
        public string CityName { get; set; }
        public string Town { get; set; }
        public string ZipCode { get; set; }
        public string StreetAddress { get; set; }

        public int ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}