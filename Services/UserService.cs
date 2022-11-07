using Microsoft.EntityFrameworkCore;
using SmartShopping.Data;
using SmartShopping.Models;

namespace SmartShopping.Services
{
    public class UserService : IUserService
    {
        private readonly DatabaseContext _context;

        public UserService(DatabaseContext context)
        {
            _context = context;
        }

        public void CreateUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public async Task CreateUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public User? GetUserByEmail(string email)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email);
        }

        public Task<User?> GetUserByEmailAsync(string email)
        {
            return _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public User? GetUserById(Guid id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id);
        }

        public Task<User?> GetUserByIdAsync(Guid id)
        {
            return _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public void UpdateUser(User user)
        {
            if (_context.Users.Contains(user))
                _context.SaveChanges();
        }

        public async Task UpdateUserAsync(User user)
        {
            if (await _context.Users.ContainsAsync(user))
                await _context.SaveChangesAsync();
        }

        public void DeleteUser(User user)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
        }

        public async Task DeleteUserAsync(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}
