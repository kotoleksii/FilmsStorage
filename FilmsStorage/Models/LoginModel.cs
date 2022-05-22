using System.ComponentModel.DataAnnotations;

namespace FilmsStorage.Models
{
    public class LoginModel
    {
        [Required]
        [Display(Name = "LoginName", ResourceType = typeof(FilmsStorage.Resources.Views.Account.Login))]
        public string LoginName { get; set; }

        [Required]
        [Display(Name = "Password", ResourceType = typeof(FilmsStorage.Resources.Views.Account.Login))]
        public string Password { get; set; }
    }
}