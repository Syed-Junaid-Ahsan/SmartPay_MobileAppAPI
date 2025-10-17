using SmartPayMobileApp_Backend.Models.DTOs;

namespace SmartPayMobileApp_Backend.Services.Interfaces
{
    public interface IBillService
    {
        Task<BillDto> CreateAsync(CreateBillRequest request);
        Task<IEnumerable<BillDto>> GetByConsumerNumberAsync(string consumerNumber);
        Task<BillDto?> GetByIdAsync(int billId);
        Task<bool> MarkPaidAsync(int billId);
        Task<IEnumerable<BillDto>> GetByConsumerNumberIdAsync(int consumerNumberId);
        Task<(IEnumerable<BillDto> items, int totalCount)> GetPagedByConsumerNumberIdAsync(int consumerNumberId, int page, int pageSize);
    }
}

