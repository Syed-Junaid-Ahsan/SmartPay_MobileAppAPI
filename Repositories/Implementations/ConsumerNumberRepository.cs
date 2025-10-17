using Microsoft.EntityFrameworkCore;
using SmartPayMobileApp_Backend.Data;
using SmartPayMobileApp_Backend.Models.Entities;
using SmartPayMobileApp_Backend.Repositories.Interfaces;

namespace SmartPayMobileApp_Backend.Repositories.Implementations
{
    public class ConsumerNumberRepository : IConsumerNumberRepository
    {
        private readonly ApplicationDbContext _context;

        public ConsumerNumberRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ConsumerNumber?> GetByIdAsync(int consumerNumberId)
        {
            return await _context.ConsumerNumbers.FirstOrDefaultAsync(c => c.ConsumerNumberId == consumerNumberId);
        }

        public async Task<ConsumerNumber?> GetByNumberAsync(string number)
        {
            return await _context.ConsumerNumbers.FirstOrDefaultAsync(c => c.Number == number);
        }

        public async Task<IEnumerable<ConsumerNumber>> GetByUserIdAsync(int userId)
        {
            return await _context.ConsumerNumbers.Where(c => c.UserId == userId).ToListAsync();
        }

        public async Task<ConsumerNumber> AddAsync(ConsumerNumber consumerNumber)
        {
            _context.ConsumerNumbers.Add(consumerNumber);
            await _context.SaveChangesAsync();
            return consumerNumber;
        }
    }
}



