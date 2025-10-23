using Microsoft.AspNetCore.Mvc;
using SmartPayMobileApp_Backend.Models.DTOs;
using SmartPayMobileApp_Backend.Services.Implementations;
using SmartPayMobileApp_Backend.Services.Interfaces;

namespace SmartPayMobileApp_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConsumerNumberController : ControllerBase
    {
        private readonly IConsumerNumberService _consumerNumberService;
        private readonly ILogger<ConsumerNumberController> _logger;

        public ConsumerNumberController(IConsumerNumberService consumerNumberService, ILogger<ConsumerNumberController> logger)
        {
            _consumerNumberService = consumerNumberService;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterConsumerNumber([FromBody] RegisterConsumerNumberRequest request)
        {
            try
            {
                var consumerNumberId = await _consumerNumberService.RegisterConsumerNumberAsync(request.userId, request.consumerNumber);
                var response = new RegisterConsumerNumberResponse { message = "Consumer number registered successfully" };
                return Created($"api/consumernumber/{consumerNumberId}", response);
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
                _logger.LogError(ex, "Error registering consumer number");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("consumer/{consumerNumberId}")]
        public async Task<ActionResult<BillPagedResponse>> GetBillsByConsumerNumberId(int consumerNumberId, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var (items, totalCount) = await _consumerNumberService.GetPagedByConsumerNumberIdAsync(consumerNumberId, page, pageSize);
                var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
                return Ok(new BillPagedResponse
                {
                    items = items,
                    totalCount = totalCount,
                    page = page,
                    pageSize = pageSize,
                    totalPages = totalPages,
                    hasNext = page < totalPages,
                    hasPrevious = page > 1
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching bills for consumerNumberId {consumerNumberId}", consumerNumberId);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<ConsumerNumberListResponse>> GetConsumerNumbersByUserId(int userId)
        {
            try
            {
                var consumerNumbers = await _consumerNumberService.GetByUserIdAsync(userId);
                return Ok(new ConsumerNumberListResponse { consumerNumbers = consumerNumbers });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching consumer numbers for user {userId}", userId);
                return StatusCode(500, "Internal server error");
            }
        }

        //[HttpGet("{consumerNumberId}")]
        //public async Task<ActionResult<ConsumerNumberDto>> GetConsumerNumber(int consumerNumberId)
        //{
        //    try
        //    {
        //        var consumerNumber = await _consumerNumberService.GetByIdAsync(consumerNumberId);
        //        if (consumerNumber == null) return NotFound();
        //        return Ok(consumerNumber);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Error fetching consumer number {consumerNumberId}", consumerNumberId);
        //        return StatusCode(500, "Internal server error");
        //    }
        //}
    }
}
