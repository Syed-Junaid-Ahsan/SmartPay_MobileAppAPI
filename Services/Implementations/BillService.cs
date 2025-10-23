using SmartPayMobileApp_Backend.Models.DTOs;
using SmartPayMobileApp_Backend.Models.Entities;
using SmartPayMobileApp_Backend.Repositories.Interfaces;
using SmartPayMobileApp_Backend.Services.Interfaces;

namespace SmartPayMobileApp_Backend.Services.Implementations
{
    public class BillService : IBillService
    {
        private readonly IBillRepository _billRepository;
        private readonly IUserRepository _userRepository;
        private readonly IConsumerNumberRepository _consumerNumberRepository;

        public BillService(IBillRepository billRepository, IUserRepository userRepository, IConsumerNumberRepository consumerNumberRepository)
        {
            _billRepository = billRepository;
            _userRepository = userRepository;
            _consumerNumberRepository = consumerNumberRepository;
        }

        //public async Task<BillDto> CreateAsync(CreateBillRequest request)
        //{
        //    if (request.amount <= 0) throw new ArgumentException("Amount must be greater than zero");
        //    if (request.dueDate < request.issueDate) throw new ArgumentException("dueDate must be on/after issueDate");
        //    if (request.expiryDate < request.dueDate) throw new ArgumentException("expiryDate must be on/after dueDate");

        //    var consumer = await _consumerNumberRepository.GetByNumberAsync(request.consumerNumber);
        //    if (consumer == null) throw new ArgumentException("Invalid consumerNumber");

        //    var bill = new Bill
        //    {
        //        BillName = request.billName,
        //        Amount = request.amount,
        //        IssueDate = request.issueDate,
        //        DueDate = request.dueDate,
        //        ExpiryDate = request.expiryDate,
        //        IsPaid = false,
        //        ConsumerNumberId = consumer.ConsumerNumberId
        //    };

        //    var created = await _billRepository.AddAsync(bill);
        //    return MapToDto(created);
        //}

        public async Task<IEnumerable<BillDto>> GetByConsumerNumberAsync(string consumerNumber)
        {
            var bills = await _billRepository.GetByConsumerNumberAsync(consumerNumber);
            return bills.Select(MapToDto);
        }

        public async Task<IEnumerable<BillDto>> GetByConsumerNumberIdAsync(int consumerNumberId)
        {
            var bills = await _billRepository.GetByConsumerNumberIdAsync(consumerNumberId);
            return bills.Select(MapToDto);
        }

        //public async Task<(IEnumerable<BillDto> items, int totalCount)> GetPagedByConsumerNumberIdAsync(int consumerNumberId, int page, int pageSize)
        //{
        //    var (items, totalCount) = await _billRepository.GetPagedByConsumerNumberIdAsync(consumerNumberId, page, pageSize);
        //    return (items.Select(MapToDto), totalCount);
        //}

        public async Task<BillDto?> GetByIdAsync(int billId)
        {
            var bill = await _billRepository.GetByIdAsync(billId);
            return bill == null ? null : MapToDto(bill);
        }

        //public async Task<bool> MarkPaidAsync(int billId)
        //{
        //    var bill = await _billRepository.GetByIdAsync(billId);
        //    if (bill == null) return false;

        //    bill.IsPaid = true;
        //    bill.UpdatedAt = DateTime.UtcNow;
        //    await _billRepository.UpdateAsync(bill);
        //    return true;
        //}

        private static BillDto MapToDto(Bill bill)
        {
            return new BillDto
            {
                billId = bill.BillId,
                billName = bill.BillName,
                amount = bill.Amount,
                dueDate = bill.DueDate,
                expiryDate = bill.ExpiryDate,
                isPaid = bill.IsPaid
            };
        }
    }
}

