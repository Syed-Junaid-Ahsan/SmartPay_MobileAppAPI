namespace SmartPayMobileApp_Backend.Services.Interfaces
{
    public interface IAuthService
    {
        Task<int> SignupAsync(string name, string phoneNumber, string email, string password, string cnicNumber);
        Task<(bool isValid, int userId)> ValidateUserAsync(string email, string password);
    }
}


