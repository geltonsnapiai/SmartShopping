using SmartShopping.Models;

namespace SmartShopping.Data
{
    public interface IUserRepository
    {
        public User Create(User user);
        public User GetUserByEmail(string email);
        public User GetUserById(Guid id);
        public void SaveChanges();
    }
}
