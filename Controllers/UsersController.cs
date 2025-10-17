//using Microsoft.AspNetCore.Mvc;
//using SmartPayMobileApp_Backend.Models.DTOs;
//using SmartPayMobileApp_Backend.Services.Interfaces;

//namespace SmartPayMobileApp_Backend.Controllers
//{
//    [ApiController]
//    [Route("api/[controller]")]
//    public class UsersController : ControllerBase
//    {
//        private readonly IUserService _userService;
//        private readonly IConsumerNumberService _consumerNumberService;
//        private readonly ILogger<UsersController> _logger;

//        public UsersController(IUserService userService, IConsumerNumberService consumerNumberService, ILogger<UsersController> logger)
//        {
//            _userService = userService;
//            _consumerNumberService = consumerNumberService;
//            _logger = logger;
//        }

//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
//        {
//            try
//            {
//                var users = await _userService.GetAllUsersAsync();
//                return Ok(users);
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Error occurred while getting users");
//                return StatusCode(500, "Internal server error");
//            }
//        }

//        [HttpGet("{id}")]
//        public async Task<ActionResult<UserDto>> GetUser(int id)
//        {
//            try
//            {
//                var user = await _userService.GetUserByIdAsync(id);
//                if (user == null)
//                    return NotFound();

//                return Ok(user);
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Error occurred while getting user with id {UserId}", id);
//                return StatusCode(500, "Internal server error");
//            }
//        }

//        [HttpPost]
//        public async Task<ActionResult<UserDto>> CreateUser(CreateUserDto createUserDto)
//        {
//            try
//            {
//                var user = await _userService.CreateUserAsync(createUserDto);
//                return CreatedAtAction(nameof(GetUser), new { id = user.id }, user);
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Error occurred while creating user");
//                return StatusCode(500, "Internal server error");
//            }
//        }

//        [HttpPut("{id}")]
//        public async Task<IActionResult> UpdateUser(int id, UpdateUserDto updateUserDto)
//        {
//            try
//            {
//                var user = await _userService.UpdateUserAsync(id, updateUserDto);
//                if (user == null)
//                    return NotFound();

//                return Ok(user);
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Error occurred while updating user with id {UserId}", id);
//                return StatusCode(500, "Internal server error");
//            }
//        }

//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteUser(int id)
//        {
//            try
//            {
//                var result = await _userService.DeleteUserAsync(id);
//                if (!result)
//                    return NotFound();

//                return NoContent();
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Error occurred while deleting user with id {UserId}", id);
//                return StatusCode(500, "Internal server error");
//            }
//        }

//        [HttpPost("{id}/consumer-numbers")] 
//        public async Task<ActionResult<RegisterConsumerNumberResponse>> RegisterConsumerNumber(int id, [FromBody] RegisterConsumerNumberRequest request)
//        {
//            try
//            {
//                if (id != request.userId) return BadRequest(new { message = "Mismatched userId" });
//                var result = await _consumerNumberService.RegisterConsumerNumberAsync(id, request.consumerNumber);
//                return Ok(result);
//            }
//            catch (ArgumentException ex)
//            {
//                return BadRequest(new { message = ex.Message });
//            }
//            catch (InvalidOperationException ex)
//            {
//                return Conflict(new { message = ex.Message });
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Error registering consumer number for user {UserId}", id);
//                return StatusCode(500, "Internal server error");
//            }
//        }

//        [HttpGet("{id}/consumer-numbers")] 
//        public async Task<ActionResult<ConsumerNumberListResponse>> ListConsumerNumbers(int id)
//        {
//            try
//            {
//                var list = await _consumerNumberService.GetByUserIdAsync(id);
//                return Ok(new ConsumerNumberListResponse { consumerNumbers = list });
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Error listing consumer numbers for user {UserId}", id);
//                return StatusCode(500, "Internal server error");
//            }
//        }
//    }
//}
