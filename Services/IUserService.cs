using SmartShopping.Models;

namespace SmartShopping.Services
{
    public interface IUserService
    {
        public void CreateUser(User user);
        public User GetUserByEmail(string email);
        public User GetUserById(Guid id);

        public void UpdateUser(User user);
    }
}
