using SmartPayMobileApp_Backend.Models.DTOs;
using SmartPayMobileApp_Backend.Models.Entities;
using SmartPayMobileApp_Backend.Repositories.Interfaces;
using SmartPayMobileApp_Backend.Services.Interfaces;

namespace SmartPayMobileApp_Backend.Services.Implementations
{
    public class ConsumerNumberService : IConsumerNumberService
    {
        private readonly IConsumerNumberRepository _repo;
        private readonly IUserRepository _userRepo;

        public ConsumerNumberService(IConsumerNumberRepository repo, IUserRepository userRepo)
        {
            _repo = repo;
            _userRepo = userRepo;
        }

        public async Task<int> RegisterConsumerNumberAsync(int userId, string consumerNumber)
        {
            if (string.IsNullOrWhiteSpace(consumerNumber)) throw new ArgumentException("consumerNumber is required");

            var user = await _userRepo.GetByIdAsync(userId);
            if (user == null || !user.IsActive) throw new ArgumentException("Invalid userId");

            var existing = await _repo.GetByNumberAsync(consumerNumber);
            if (existing != null && existing.UserId == userId)
                throw new InvalidOperationException("Consumer number already registered");
            if (existing != null && existing.UserId != userId)
                throw new InvalidOperationException("Invalid Consumer number!");

            var cn = new ConsumerNumber { Number = consumerNumber, UserId = userId };
            await _repo.AddAsync(cn);
            return cn.ConsumerNumberId;
        }

        public async Task<IEnumerable<ConsumerNumberDto>> GetByUserIdAsync(int userId)
        {
            var list = await _repo.GetByUserIdAsync(userId);
            return list.Select(c => new ConsumerNumberDto { consumerNumberId = c.ConsumerNumberId, number = c.Number });
        }

        public async Task<ConsumerNumberDto?> GetByIdAsync(int consumerNumberId)
        {
            var consumerNumber = await _repo.GetByIdAsync(consumerNumberId);
            if (consumerNumber == null) return null;
            return new ConsumerNumberDto { consumerNumberId = consumerNumber.ConsumerNumberId, number = consumerNumber.Number };
        }
    }
}



