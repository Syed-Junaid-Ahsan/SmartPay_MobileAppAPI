using Microsoft.AspNetCore.Mvc;
using SmartPayMobileApp_Backend.Models.DTOs;
using SmartPayMobileApp_Backend.Repositories.Interfaces;
using SmartPayMobileApp_Backend.Services.Implementations;
using SmartPayMobileApp_Backend.Services.Interfaces;

namespace SmartPayMobileApp_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserRepository _userRepository;

        private readonly IUserService _userService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthService authService, IUserRepository userRepository, IUserService userService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _userRepository = userRepository;
            _userService = userService;
            _logger = logger;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> Signup([FromBody] SignupRequest request)
        {
            try
            {
                var userId = await _authService.SignupAsync(request.name, request.phoneNumber, request.email, request.password, request.cnicNumber);
                var response = new SignupResponse { message = "Signup Successful" };
                return Created($"api/users/{userId}", response);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during signup");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                var (isValid, userId) = await _authService.ValidateUserAsync(request.email, request.password);
                if (!isValid) return Unauthorized(new { message = "Invalid credentials" });

                var user = await _userRepository.GetByIdAsync(userId);
                var response = new LoginResponse { message = "Login successful", userId = userId, name = user.Name, email = user.Email, phoneNumber = user.PhoneNumber, cnicNumber = user.CnicNumber };
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during login");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
