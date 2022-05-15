using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FilmsStorage.Validators
{
    public class AuditPasswordAttribute:ValidationAttribute
    {
        public int MinimumLength { get; set; } = 8;
        public bool RequireUpperLowerMix { get; set; } = false;
        public override bool IsValid(object value)
        {
            if (value == null)
                return false;

            if(value is string)
            {
                string password = value as string;

                if (password.Length < MinimumLength)
                {
                    //ErrorMessage = "Password is too short";

                    return false;
                }

                if (RequireUpperLowerMix)
                {
                    //TODO: Check it
                    //return result
                }
            }

            return false;
        }
    }
}