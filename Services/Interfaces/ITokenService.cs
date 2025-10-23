namespace SmartPayMobileApp_Backend.Services.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(int userId, string email, string consumerNumber);
    }
}
