using SmartPayMobileApp_Backend.Models.Entities;
using SmartPayMobileApp_Backend.Repositories.Interfaces;
using Dapper;
using System.Data;

namespace SmartPayMobileApp_Backend.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbConnection _connection;

        public UserRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            const string sql = @"SELECT id AS Id, name AS Name, email AS Email, phoneNumber AS PhoneNumber, passwordHash AS PasswordHash, cnicNumber AS CnicNumber, deviceId AS DeviceId, createdAt AS CreatedAt, updatedAt AS UpdatedAt, isActive AS IsActive FROM CustomerUser WHERE isActive = 1";
            return await _connection.QueryAsync<User>(sql);
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            const string sql = @"SELECT id AS Id, name AS Name, email AS Email, phoneNumber AS PhoneNumber, passwordHash AS PasswordHash, cnicNumber AS CnicNumber, deviceId AS DeviceId, createdAt AS CreatedAt, updatedAt AS UpdatedAt, isActive AS IsActive FROM CustomerUser WHERE id = @id AND isActive = 1";
            return await _connection.QueryFirstOrDefaultAsync<User>(sql, new { id });
        }

        

        public async Task<User?> GetByPhoneNumberAsync(string phoneNumber)
        {
            const string sql = @"SELECT TOP 1 id AS Id, name AS Name, email AS Email, phoneNumber AS PhoneNumber, passwordHash AS PasswordHash, cnicNumber AS CnicNumber, deviceId AS DeviceId, createdAt AS CreatedAt, updatedAt AS UpdatedAt, isActive AS IsActive FROM CustomerUser WHERE phoneNumber = @phoneNumber AND isActive = 1";
            return await _connection.QueryFirstOrDefaultAsync<User>(sql, new { phoneNumber });
        }

        public async Task<User?> GetByCnicNumberAsync(string cnicNumber)
        {
            const string sql = @"SELECT TOP 1 id AS Id, name AS Name, email AS Email, phoneNumber AS PhoneNumber, passwordHash AS PasswordHash, cnicNumber AS CnicNumber, deviceId AS DeviceId, createdAt AS CreatedAt, updatedAt AS UpdatedAt, isActive AS IsActive FROM CustomerUser WHERE cnicNumber = @cnicNumber AND isActive = 1";
            return await _connection.QueryFirstOrDefaultAsync<User>(sql, new { cnicNumber });
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            const string sql = @"SELECT TOP 1 id AS Id, name AS Name, email AS Email, phoneNumber AS PhoneNumber, passwordHash AS PasswordHash, cnicNumber AS CnicNumber, deviceId AS DeviceId, createdAt AS CreatedAt, updatedAt AS UpdatedAt, isActive AS IsActive FROM CustomerUser WHERE email = @email AND isActive = 1";
            return await _connection.QueryFirstOrDefaultAsync<User>(sql, new { email });
        }

        public async Task<User> AddAsync(User user)
        {
            const string sql = @"INSERT INTO CustomerUser (name, email, phoneNumber, passwordHash, cnicNumber, deviceId, createdAt, isActive)
                                  VALUES (@Name, @Email, @PhoneNumber, @PasswordHash, @CnicNumber, @DeviceId, @CreatedAt, @IsActive);
                                  SELECT CAST(SCOPE_IDENTITY() as int);";
            var newId = await _connection.ExecuteScalarAsync<int>(sql, user);
            user.Id = newId;
            return user;
        }

        public async Task<User> UpdateAsync(User user)
        {
            const string sql = @"UPDATE CustomerUser SET name = @Name, email = @Email, phoneNumber = @PhoneNumber, passwordHash = @PasswordHash, cnicNumber = @CnicNumber, deviceId = @DeviceId, updatedAt = @UpdatedAt, isActive = @IsActive WHERE id = @Id";
            await _connection.ExecuteAsync(sql, user);
            return user;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            const string sql = @"DELETE FROM CustomerUser WHERE id = @id";
            var rows = await _connection.ExecuteAsync(sql, new { id });
            return rows > 0;
        }
    }
}
