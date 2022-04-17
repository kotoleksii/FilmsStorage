using System.ComponentModel.DataAnnotations;

namespace FilmsStorage.Models
{
    public class LoginModel
    {
        [Required]
        public string LoginName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}