using Microsoft.EntityFrameworkCore;
using SmartPayMobileApp_Backend.Data;
using SmartPayMobileApp_Backend.Models.Entities;
using SmartPayMobileApp_Backend.Repositories.Interfaces;

namespace SmartPayMobileApp_Backend.Repositories.Implementations
{
    public class BillRepository : IBillRepository
    {
        private readonly ApplicationDbContext _context;

        public BillRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Bill> AddAsync(Bill bill)
        {
            _context.Bills.Add(bill);
            await _context.SaveChangesAsync();
            return bill;
        }

        public async Task<Bill?> GetByIdAsync(int billId)
        {
            return await _context.Bills.FirstOrDefaultAsync(b => b.BillId == billId);
        }


        public async Task<IEnumerable<Bill>> GetByConsumerNumberAsync(string consumerNumber)
        {
            return await _context.Bills
                .Include(b => b.ConsumerNumber)
                .Where(b => b.ConsumerNumber != null && b.ConsumerNumber.Number == consumerNumber)
                .ToListAsync();
        }

        public async Task<IEnumerable<Bill>> GetByConsumerNumberIdAsync(int consumerNumberId)
        {
            return await _context.Bills
                .Where(b => b.ConsumerNumberId == consumerNumberId)
                .ToListAsync();
        }

        public async Task<(IEnumerable<Bill> items, int totalCount)> GetPagedByConsumerNumberIdAsync(int consumerNumberId, int page, int pageSize)
        {
            if (page < 1) page = 1;
            if (pageSize < 1) pageSize = 10;

            var query = _context.Bills.Where(b => b.ConsumerNumberId == consumerNumberId)
                .OrderBy(b => b.DueDate);

            var totalCount = await query.CountAsync();
            var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            return (items, totalCount);
        }

        public async Task<Bill> UpdateAsync(Bill bill)
        {
            _context.Bills.Update(bill);
            await _context.SaveChangesAsync();
            return bill;
        }
    }
}

