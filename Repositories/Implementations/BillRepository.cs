using SmartPayMobileApp_Backend.Models.Entities;
using SmartPayMobileApp_Backend.Repositories.Interfaces;
using Dapper;
using System.Data;

namespace SmartPayMobileApp_Backend.Repositories.Implementations
{
    public class BillRepository : IBillRepository
    {
        private readonly IDbConnection _connection;

        public BillRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<Bill> AddAsync(Bill bill)
        {
            // Since we're using existing SP_Bill table, this might not be needed
            // or you might want to create a different SP for adding bills
            const string sql = @"INSERT INTO SP_Bill (Consumer_Detail, Amount, DueDate1, ExpDate, BillStatus, Consumer_Number)
                                  VALUES (@BillName, @Amount, @DueDate, @ExpiryDate, CASE WHEN @IsPaid = 1 THEN 5 ELSE 4 END, @ConsumerNumber);
                                  SELECT CAST(SCOPE_IDENTITY() as int);";
            var newId = await _connection.ExecuteScalarAsync<int>(sql, new { 
                bill.BillName, 
                bill.Amount, 
                bill.DueDate, 
                bill.ExpiryDate, 
                bill.IsPaid, 
                ConsumerNumber = "" // You'll need to pass this from the service
            });
            bill.BillId = newId;
            return bill;
        }

        public async Task<Bill?> GetByIdAsync(int billId)
        {
            const string proc = "sp_GetBillById";
            return await _connection.QueryFirstOrDefaultAsync<Bill>(proc, new { billId }, commandType: CommandType.StoredProcedure);
        }


        public async Task<IEnumerable<Bill>> GetByConsumerNumberAsync(string consumerNumber)
        {
            const string proc = "sp_GetBillsByConsumerNumber";
            return await _connection.QueryAsync<Bill>(proc, new { consumerNumber }, commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<Bill>> GetByConsumerNumberIdAsync(int consumerNumberId)
        {
            const string proc = "sp_GetBillsByConsumerNumberId";
            return await _connection.QueryAsync<Bill>(proc, new { consumerNumberId }, commandType: CommandType.StoredProcedure);
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

        public async Task<Bill> UpdateAsync(Bill bill)
        {
            const string sql = @"UPDATE Bills SET billName = @BillName, amount = @Amount, issueDate = @IssueDate, dueDate = @DueDate, expiryDate = @ExpiryDate, isPaid = @IsPaid, consumerNumberId = @ConsumerNumberId, updatedAt = @UpdatedAt WHERE billId = @BillId";
            await _connection.ExecuteAsync(sql, bill);
            return bill;
        }
    }
}

