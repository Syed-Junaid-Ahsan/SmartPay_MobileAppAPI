using SmartPayMobileApp_Backend.Models.Entities;

namespace SmartPayMobileApp_Backend.Repositories.Interfaces
{
    public interface IConsumerNumberRepository
    {
        Task<ConsumerNumber?> GetByIdAsync(int consumerNumberId);
        Task<ConsumerNumber?> GetByNumberAsync(string number);
        Task<IEnumerable<ConsumerNumber>> GetByUserIdAsync(int userId);
        Task<ConsumerNumber> AddAsync(ConsumerNumber consumerNumber);
        Task<(IEnumerable<Bill> items, int totalCount)> GetPagedByConsumerNumberIdAsync(int consumerNumberId, int page, int pageSize);

    }
}



