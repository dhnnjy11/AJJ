using System.ComponentModel.DataAnnotations;

namespace Ajj.Areas.Clients.Models
{
    public class ClientAccountViewModel
    {
        //public int Id { get; set; }
        public string UserName { get; set; }

        [Required,Display(Name = "パスワード")]        
        public string OldPassword { get; set; }

        [Required, Display(Name = "新しいパスワード")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "パスワード確認")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}