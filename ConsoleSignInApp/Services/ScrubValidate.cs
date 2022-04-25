using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleSignInApp.Services
{
    public class ScrubValidate : IScrubValidate
    {
        public List<string> ValidateErrors = new List<string>();

        public bool isValidPassword(string password)
        {
            if (!isValidPasswordLength(password)) return false;

            if (!isValidPasswordFormat(password)) return false;

            if (hasBadCharacters(password)) return false;

            if(!isValidPasswordFormat(password)) return false;  

            return true;
        }

        public bool isValidPasswordLength(string Password)
        {
            try
            {
                if (Password == null)
                { ValidateErrors.Add("Password Length Does Not Match Requirements"); return false; }

                string pwd = Password.Trim();

                if (pwd.Length > Config.PasswordLengthMax || Password.Length < Config.PasswordLengthMin)
                { ValidateErrors.Add("Password Length Does Not Match Requirements"); return false; }

                return true;
            }
            catch (Exception ex)
            {
                var errorMessage = $"isValidPasswordLength Error {ex.Message}";
                ValidateErrors.Add("Password Validation Error");
                return false;
            }
        }

        public bool isValidUserIdLength(string UserID)
        {
            try
            {
                if (UserID == null)
                { ValidateErrors.Add("User Length Does Not Match Requirements"); return false; }

                string pwd = UserID.Trim();

                if (pwd.Length > Config.UserIdLengthMax || UserID.Length < Config.UserIdLengthMin)
                { ValidateErrors.Add("User Length Does Not Match Requirements"); return false; }

                return true;
            }
            catch (Exception ex)
            {
                var errorMessage = $"isValidUserLength Error {ex.Message}";
                ValidateErrors.Add("UserName Validation Error");
                return false;
            }
        }

        public bool hasBadCharacters(string StringToCheck)
        {
            bool notValid = false;
            try
            {
                notValid = StringToCheck.ToLower().Contains("BadWord");
                notValid = StringToCheck.ToLower().Contains("exec");
                notValid = StringToCheck.Contains("'");
                notValid = StringToCheck.Contains("--");
                notValid = StringToCheck.Contains("/*");
                notValid = StringToCheck.Contains("*/");

                if (notValid)
                {
                    ValidateErrors.Add("Entry Invalid");
                }
                return notValid;
            }
            catch (Exception ex)
            {
                var errorMessage = $"ScrubBadCharacters Error {ex.Message}";
                ValidateErrors.Add("Password Validation Error");
                return false;
            }
        }

        public bool isValidPasswordFormat(string password)
        {
            var input = password;
            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasLowerChar = new Regex(@"[a-z]+");
            var hasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");

            if (!hasLowerChar.IsMatch(input))
            {
                ValidateErrors.Add("Password should contain at least one lower case letter.");
            }

            if (!hasUpperChar.IsMatch(input))
            {
                ValidateErrors.Add("Password should contain at least one upper case letter.");
            }

            if (!hasNumber.IsMatch(input))
            {
                ValidateErrors.Add("Password should contain at least one numeric value.");
            }

            if (!hasSymbols.IsMatch(input))
            {
                ValidateErrors.Add("Password should contain at least one special case character.");
            }

            return (ValidateErrors.Count > 0) ? false : true;
        }

        public List<string> GetPasswordErrors()
        {
            return this.ValidateErrors;
        }
    }
}
