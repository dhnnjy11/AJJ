using System.ComponentModel.DataAnnotations;

namespace Ajj.ViewModels.HomeViewModel
{
    public class SendQueryViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Compare("Email", ErrorMessage = "The Email and confirmation Email do not match.")]
        [EmailAddress]
        public string ConfirmEmail { get; set; }

        [Required]
        public string Subject { get; set; }

        public string Description { get; set; }
    }
}