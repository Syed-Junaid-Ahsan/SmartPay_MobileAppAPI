using SmartPayMobileApp_Backend.Models.Entities;
using SmartPayMobileApp_Backend.Repositories.Interfaces;
using Dapper;
using System.Data;

namespace SmartPayMobileApp_Backend.Repositories.Implementations
{
    public class ConsumerNumberRepository : IConsumerNumberRepository
    {
        private readonly IDbConnection _connection;

        public ConsumerNumberRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<ConsumerNumber?> GetByIdAsync(int consumerNumberId)
        {
            const string sql = @"SELECT ConsumerNumberId, Number, CustomerUserId FROM ConsumerNumbers WHERE ConsumerNumberId = @consumerNumberId";
            return await _connection.QueryFirstOrDefaultAsync<ConsumerNumber>(sql, new { consumerNumberId });
        }

        public async Task<ConsumerNumber?> GetByNumberAsync(string number)
        {
            const string sql = @"SELECT TOP 1 ConsumerNumberId, Number, CustomerUserId FROM ConsumerNumbers WHERE Number = @number";
            return await _connection.QueryFirstOrDefaultAsync<ConsumerNumber>(sql, new { number });
        }

        public async Task<IEnumerable<ConsumerNumber>> GetByUserIdAsync(int userId)
        {
            const string sql = @"SELECT ConsumerNumberId, Number, CustomerUserId FROM ConsumerNumbers WHERE CustomerUserId = @userId";
            return await _connection.QueryAsync<ConsumerNumber>(sql, new { userId });
        }

        public async Task<ConsumerNumber> AddAsync(ConsumerNumber consumerNumber)
        {
            const string sql = @"INSERT INTO ConsumerNumbers (Number, CustomerUserId) VALUES (@Number, @CustomerUserId); SELECT CAST(SCOPE_IDENTITY() as int);";
            var newId = await _connection.ExecuteScalarAsync<int>(sql, consumerNumber);
            consumerNumber.ConsumerNumberId = newId;
            return consumerNumber;
        }
        public async Task<(IEnumerable<Bill> items, int totalCount)> GetPagedByConsumerNumberIdAsync(int consumerNumberId, int page, int pageSize)
        {
            if (page < 1) page = 1;
            if (pageSize < 1) pageSize = 10;

            const string proc = "sp_GetBillsByConsumerNumberId_Paged";
            using var multi = await _connection.QueryMultipleAsync(proc, new { consumerNumberId, page, pageSize }, commandType: CommandType.StoredProcedure);
            var items = await multi.ReadAsync<Bill>();
            var total = await multi.ReadFirstAsync<int>();
            return (items, total);
        }
    }
}



