using SmartPayMobileApp_Backend.Models.DTOs;

namespace SmartPayMobileApp_Backend.Services.Interfaces
{
    public interface IConsumerNumberService
    {
        Task<int> RegisterConsumerNumberAsync(int userId, string consumerNumber);
        Task<IEnumerable<ConsumerNumberDto>> GetByUserIdAsync(int userId);
        Task<ConsumerNumberDto?> GetByIdAsync(int consumerNumberId);
    }
}



