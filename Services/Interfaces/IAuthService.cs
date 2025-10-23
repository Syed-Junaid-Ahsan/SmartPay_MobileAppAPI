namespace SmartPayMobileApp_Backend.Services.Interfaces
{
    public interface IAuthService
    {
        Task<int> SignupAsync(string name, string phoneNumber, string email, string password, string cnicNumber, string deviceId);
        Task<(bool isValid, int userId, bool deviceChanged)> ValidateUserAsync(string email, string password, string deviceId);
    }
}


