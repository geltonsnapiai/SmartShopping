using SmartShopping.Models;

namespace SmartShopping.Services
{
    public interface IUserService
    {
        public void CreateUser(User user);
        public User? GetUserByEmail(string email);
        public User? GetUserById(Guid id);
        public void UpdateUser(User user);
        public void DeleteUser(User user);

        public Task CreateUserAsync(User user);
        public Task<User?> GetUserByEmailAsync(string email);
        public Task<User?> GetUserByIdAsync(Guid id);
        public Task UpdateUserAsync(User user);
        public Task DeleteUserAsync(User user);
    }
}
