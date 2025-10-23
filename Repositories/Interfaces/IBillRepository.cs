using SmartPayMobileApp_Backend.Models.Entities;

namespace SmartPayMobileApp_Backend.Repositories.Interfaces
{
    public interface IBillRepository
    {
        Task<Bill> AddAsync(Bill bill);
        Task<Bill?> GetByIdAsync(int billId);
        Task<IEnumerable<Bill>> GetByConsumerNumberAsync(string consumerNumber);
        Task<IEnumerable<Bill>> GetByConsumerNumberIdAsync(int consumerNumberId);
        //Task<(IEnumerable<Bill> items, int totalCount)> GetPagedByConsumerNumberIdAsync(int consumerNumberId, int page, int pageSize);
        Task<Bill> UpdateAsync(Bill bill);
    }
}

