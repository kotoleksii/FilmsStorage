using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace FilmsStorage.Validators
{
    public class AuditPasswordAttribute:ValidationAttribute
    {
        public int MinimumLength { get; set; } = 8;
        public bool RequireUpperLowerMix { get; set; } = false;
        public int SpecialCharactersCount { get; set; } = 1;
        public bool RequireSpecialCharacters { get; set; } = false;

        public override bool IsValid(object value)
        {
            if (value == null)
                return false;

            if(value is string)
            {
                string password = value as string;
                string upperLowerMixRegex = "(?=.*?[A-Z])(?=.*?[a-z])";
                string specialCharacterRegex = $"(?=(.*[@$#!%*?&]){{{SpecialCharactersCount}}})";

                if (password.Length < MinimumLength)
                {
                    ErrorMessage = "Password is too short";

                    return false;
                }

                if (RequireUpperLowerMix && RequireSpecialCharacters)
                {
                    ErrorMessage = $"Password must be case sensitive or requires {SpecialCharactersCount} special characters";

                    var result = new Regex(upperLowerMixRegex + specialCharacterRegex);
                    return result.IsMatch(password);
                }

                if (RequireUpperLowerMix)
                {
                    ErrorMessage = "Password must be case sensitive";

                    var result = new Regex(upperLowerMixRegex);
                    return result.IsMatch(password);
                }

                if (RequireSpecialCharacters)
                {
                    ErrorMessage = $"Password requires {SpecialCharactersCount} special characters";

                    var result = new Regex(specialCharacterRegex); 
                    return result.IsMatch(password);
                }

            }

            return false;
        }
    }
}