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
        public string UserName { get; set; }

        [Required]
        public string LoginName { get; set; }

        [Required]
        [AuditPassword(MinimumLength = 10, RequireUpperLowerMix = true, ErrorMessage = "pass is too short")]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string PasswordRepetition { get; set; }
    }
}