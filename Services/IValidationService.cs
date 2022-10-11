using SmartShopping.Dtos;

namespace SmartShopping.Services
{
    public interface IValidationService
    {
        public bool ValidateRegistration(RegisterDto dto, out string? errorMessage);
        public bool ValidateUsername(string username, out string? errorMessage);
        public bool ValidateEmail(string email, out string? errorMessage);
        public bool ValidatePassword(string password, out string? errorMessage);

        public Task<(bool Ok, string invalidField, string? ErrorMessage)> ValidateRegistrationAsync(RegisterDto dto);
        public Task<(bool Ok, string? ErrorMessage)> ValidateEmailAsync(string email);
    }
}
