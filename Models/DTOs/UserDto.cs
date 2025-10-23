namespace SmartPayMobileApp_Backend.Models.DTOs
{
    public class UserDto
    {
        public int id { get; set; }
        public string name { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public string phoneNumber { get; set; } = string.Empty;
        public DateTime createdAt { get; set; }
        public bool isActive { get; set; }
    }

    public class CreateUserDto
    {
        public string name { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public string phoneNumber { get; set; } = string.Empty;
    }

    public class UpdateUserDto
    {
        public string name { get; set; } = string.Empty;
        public string phoneNumber { get; set; } = string.Empty;
    }

    public class SignupRequest
    {
        public string name { get; set; } = string.Empty;
        public string phoneNumber { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
        public string cnicNumber { get; set; } = string.Empty;
        public string deviceId { get; set; } = string.Empty;
    }

    public class SignupResponse
    {
        public string message { get; set; } = string.Empty;
    }

    public class LoginRequest
    {
        public string email { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
        public string deviceId { get; set; } = string.Empty;
    }

    public class LoginResponse
    {
        public string message { get; set; } = string.Empty;
        public int userId { get; set; }
        public string name { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public string phoneNumber { get; set; } = string.Empty;
        public string cnicNumber { get; set; } = string.Empty;
        public string deviceId { get; set; } = string.Empty;
        public bool deviceChanged { get; set; }
        public bool isActive { get; set; }

    }
}
