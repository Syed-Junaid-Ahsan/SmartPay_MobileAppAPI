using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Logging;
using SmartPayMobileApp_Backend.Models.Entities;
using SmartPayMobileApp_Backend.Repositories.Interfaces;
using SmartPayMobileApp_Backend.Services.Interfaces;

namespace SmartPayMobileApp_Backend.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<AuthService> _logger;

        public AuthService(IUserRepository userRepository, ILogger<AuthService> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<int> SignupAsync(string name, string phoneNumber, string email, string password, string cnicNumber, string deviceId)
        {
            if (!IsValidEmail(email))
                throw new ArgumentException("Invalid email format");

            if (!IsValidCnic(cnicNumber))
                throw new ArgumentException("Invalid CNIC format");

            if (!IsValidPhoneNumber(phoneNumber))
                throw new ArgumentException("Invalid Phone Number format");

            var existing = await _userRepository.GetByEmailAsync(email);
            if (existing != null)
                throw new InvalidOperationException("Email already exists");

            var existingPhone = await _userRepository.GetByPhoneNumberAsync(phoneNumber);
            if (existingPhone != null)
                throw new InvalidOperationException("Phone number already exists");

            var existingCnic = await _userRepository.GetByCnicNumberAsync(cnicNumber);
            if (existingCnic != null)
                throw new InvalidOperationException("CNIC already exists");

            var normalizedPassword = NormalizeUtf16LeString(password);
            var passwordHash = HashPassword(normalizedPassword);

            var user = new User
            {
                Name = name,
                PhoneNumber = phoneNumber,
                Email = email,
                PasswordHash = passwordHash,
                CnicNumber = cnicNumber,
                DeviceId = deviceId,
            };

            await _userRepository.AddAsync(user);
            return user.Id;
        }

        public async Task<(bool isValid, int userId, bool deviceChanged)> ValidateUserAsync(string email, string password, string deviceId)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null || !user.IsActive)
                return (false, 0, false);

            // Normalize potential Base64 UTF-16LE encoded password from frontend
            //var normalizedPassword = NormalizeIncomingPassword(password);

            var normalizedPassword = NormalizeUtf16LeString(password);

            var ok = VerifyPassword(normalizedPassword, user.PasswordHash);
            if (!ok) return (false, 0, false);

            var deviceChanged = false;
            if (!string.IsNullOrWhiteSpace(deviceId) && !string.Equals(user.DeviceId, deviceId, StringComparison.Ordinal))
            {
                deviceChanged = true;
                user.DeviceId = deviceId;
                user.UpdatedAt = DateTime.UtcNow;
                await _userRepository.UpdateAsync(user);
            }

            return (true, user.Id, deviceChanged);
        }

        private static string NormalizeUtf16LeString(string incoming)
        {
            if (string.IsNullOrEmpty(incoming))
                return incoming;

            // 🔹 detect if the incoming contains raw bytes (comma-separated or brackets)
            // like "[49, 0, 50, 0, 51, 0, 113, 0, 119, 0, 101, 0]"
            if (incoming.Contains("[") && incoming.Contains("]"))
            {
                try
                {
                    // Remove brackets and spaces
                    var byteStrings = incoming.Trim('[', ']').Split(',');
                    var bytes = byteStrings.Select(b => Convert.ToByte(b.Trim())).ToArray();

                    // Decode bytes as UTF-16 LE
                    return Encoding.Unicode.GetString(bytes);
                }
                catch
                {
                    // fallback if not parsable
                    return incoming;
                }
            }

            // 🔹 If frontend sends raw UTF-16 LE bytes in string form, try direct decoding
            try
            {
                var bytes = Encoding.UTF8.GetBytes(incoming);
                var decoded = Encoding.Unicode.GetString(bytes);
                if (!string.IsNullOrWhiteSpace(decoded))
                    return decoded;
            }
            catch
            {
                // ignore errors
            }

            return incoming;
        }

        //private static string NormalizeIncomingPassword(string incoming)
        //{
        //    if (string.IsNullOrEmpty(incoming)) return incoming;

        //    // Try to treat input as Base64 of UTF-16LE (Encoding.Unicode)
        //    try
        //    {
        //        // Trim whitespace that may be added by transport
        //        var trimmed = incoming.Trim();
        //        // Base64 strings must have length % 4 == 0; pad if clearly missing padding
        //        int mod4 = trimmed.Length % 4;
        //        if (mod4 != 0)
        //        {
        //            trimmed = trimmed.PadRight(trimmed.Length + (4 - mod4), '=');
        //        }

        //        var raw = Convert.FromBase64String(trimmed);
        //        // Decode as UTF-16LE
        //        var decoded = Encoding.Unicode.GetString(raw);
        //        // Heuristic: if decoded is non-empty and contains printable characters, use it
        //        if (!string.IsNullOrEmpty(decoded))
        //        {
        //            return decoded;
        //        }
        //    }
        //    catch
        //    {
        //        // Not base64 or not decodable as UTF-16LE; fall back to original
        //    }

        //    return incoming;
        //}

        private static string HashPassword(string password)
        {
            // PBKDF2 with HMACSHA256
            using var rng = RandomNumberGenerator.Create();
            var salt = new byte[16];
            rng.GetBytes(salt);

            const int iterations = 100000;
            using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256);
            var hash = pbkdf2.GetBytes(32);

            // store as: iterations.saltBase64.hashBase64
            var result = $"{iterations}.{Convert.ToBase64String(salt)}.{Convert.ToBase64String(hash)}";
            return result;
        }

        private static bool VerifyPassword(string password, string stored)
        {
            var parts = stored.Split('.', 3);
            if (parts.Length != 3) return false;

            var iterations = int.Parse(parts[0]);
            var salt = Convert.FromBase64String(parts[1]);
            var expectedHash = Convert.FromBase64String(parts[2]);

            using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256);
            var hash = pbkdf2.GetBytes(32);
            return CryptographicOperations.FixedTimeEquals(hash, expectedHash);
        }

        private static bool IsValidEmail(string email)
        {
            // r'^[\w-\.]+@([\w-]+\.)+[\w]{2,4}$'
            var regex = new System.Text.RegularExpressions.Regex("^[\\w-\\.]+@([\\w-]+\\.)+[\\w]{2,4}$");
            return regex.IsMatch(email);
        }

        private static bool IsValidCnic(string cnic)
        {
            // exactly 13 digits
            var regex = new System.Text.RegularExpressions.Regex("^[0-9]{13}$");
            return regex.IsMatch(cnic);
        }

        private static bool IsValidPhoneNumber(string phoneNumber)
        {
            // exactly 13 digits
            var regex = new System.Text.RegularExpressions.Regex("^(?:92|0)[0-9]{10}$");
            return regex.IsMatch(phoneNumber);
        }
    }
}
