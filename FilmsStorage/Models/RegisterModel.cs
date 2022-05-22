using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using FilmsStorage.Validators;

namespace FilmsStorage.Models
{
    public class RegisterModel
    {
        [Required]
        [Display(Name = "UserName", ResourceType = typeof(FilmsStorage.Resources.Views.Account.Register))]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "LoginName", ResourceType = typeof(FilmsStorage.Resources.Views.Account.Register))]
        public string LoginName { get; set; }

        [Required]
        [AuditPassword(MinimumLength = 10, RequireUpperLowerMix = true, RequireSpecialCharacters = true, SpecialCharactersCount = 3)]
        [Display(Name = "Password", ResourceType = typeof(FilmsStorage.Resources.Views.Account.Register))]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        [Display(Name = "PasswordRepetition", ResourceType = typeof(FilmsStorage.Resources.Views.Account.Register))]
        public string PasswordRepetition { get; set; }
    }
}