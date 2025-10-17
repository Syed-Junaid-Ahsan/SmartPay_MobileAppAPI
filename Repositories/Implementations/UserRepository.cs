using Microsoft.EntityFrameworkCore;
using SmartPayMobileApp_Backend.Data;
using SmartPayMobileApp_Backend.Models.Entities;
using SmartPayMobileApp_Backend.Repositories.Interfaces;

namespace SmartPayMobileApp_Backend.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users
                .Where(u => u.IsActive)
                .ToListAsync();
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Id == id && u.IsActive);
        }

        

        public async Task<User?> GetByPhoneNumberAsync(string phoneNumber)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.PhoneNumber == phoneNumber && u.IsActive);
        }

        public async Task<User?> GetByCnicNumberAsync(string cnicNumber)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.CnicNumber == cnicNumber && u.IsActive);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email && u.IsActive);
        }

        public async Task<User> AddAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
