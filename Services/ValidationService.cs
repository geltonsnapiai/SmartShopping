using Microsoft.IdentityModel.Tokens;
using SmartShopping.Dtos;
using System.Text.RegularExpressions;

namespace SmartShopping.Services
{
    public class ValidationService : IValidationService
    {
        private readonly IUserService _userService;
        private static readonly Regex regex = new Regex(
            @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
            + "@"
            + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$");
        // ugly

        public ValidationService(IUserService userService)
        {
            _userService = userService;
        }

        public bool ValidateRegistration(RegisterDto dto, out string? errorMessage)
        {
            if (!ValidateUsername(dto.Name, out errorMessage)) return false;
            if (!ValidateEmail(dto.Email, out errorMessage)) return false;
            if (!ValidatePassword(dto.Password, out errorMessage)) return false;
            return true;
        }

        public async Task<(bool Ok, string invalidField, string? ErrorMessage)> ValidateRegistrationAsync(RegisterDto dto)
        {
            string? errorMessage;
            if (!ValidateUsername(dto.Name, out errorMessage)) return (false, "username", errorMessage);
            var (ok, err) = await ValidateEmailAsync(dto.Email);
            if (!ok) return (false,"email", err);
            if (!ValidatePassword(dto.Password, out errorMessage)) return (false, "password", errorMessage);
            return (true, "", null);
        }

        public bool ValidateUsername(string username, out string? errorMessage)
        {
            if (username.IsNullOrEmpty())
            {
                errorMessage = "Username is empty";
                return false;
            }

            errorMessage = null;
            return true;
        }

        public bool ValidateEmail(string email, out string? errorMessage)
        {
            if (email.IsNullOrEmpty())
            {
                errorMessage = "Email is empty";
                return false;
            }

            if (!regex.IsMatch(email))
            {
                errorMessage = "Invalid email";
                return false;
            }

            var user = _userService.GetUserByEmail(email);
            if (user is not null)
            {
                errorMessage = "Email is taken";
                return false;
            }

            errorMessage = null;
            return true;
        }

        public async Task<(bool Ok, string? ErrorMessage)> ValidateEmailAsync(string email)
        {
            string? errorMessage = null;

            if (email.IsNullOrEmpty())
            {
                errorMessage = "Email is empty";
                return (false, errorMessage);
            }

            if (!regex.IsMatch(email))
            {
                errorMessage = "Invalid email";
                return (false, errorMessage);
            }

            var user = await _userService.GetUserByEmailAsync(email);
            if (user is not null)
            {
                errorMessage = "Email is taken";
                return (false, errorMessage);
            }

            return (true, null);
        }

        public bool ValidatePassword(string password, out string? errorMessage)
        {
            if (password.IsNullOrEmpty())
            {
                errorMessage = "Password is empty";
                return false;
            }

            if (!Regex.IsMatch(password, "(?=.*[a-z])"))
            {
                errorMessage = "Password must contain atleast one lower case character";
                return false;
            }

            if (!Regex.IsMatch(password, "(?=.*[A-Z])"))
            {
                errorMessage = "Password must contain atleast one upper case character";
                return false;
            }

            if (!Regex.IsMatch(password, "(?=.*\\d)"))
            {
                errorMessage = "Password must contain atleast one digit";
                return false;
            }

            if (!Regex.IsMatch(password, "(?=.*[-+_!@#$%^&*.,?])"))
            {
                errorMessage = "Password must contain atleast one of the special characters (-+_!@#$%^&*.,?)";
                return false;
            }

            errorMessage = null;
            return true;
        }
    }
}
