namespace SmartShopping.Services
{
    public interface IValidationService
    {
        public bool ValidateUsername(string username, out string? errorMessage);
        public bool ValidateEmail(string email, out string? errorMessage);
        public bool ValidatePassword(string password, out string? errorMessage);
    }
}
