using Microsoft.AspNetCore.Mvc;
using SmartPayMobileApp_Backend.Models.DTOs;
using SmartPayMobileApp_Backend.Services.Interfaces;

namespace SmartPayMobileApp_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BillsController : ControllerBase
    {
        private readonly IBillService _billService;
        private readonly ILogger<BillsController> _logger;

        public BillsController(IBillService billService, ILogger<BillsController> logger)
        {
            _billService = billService;
            _logger = logger;
        }
        
        //[HttpGet]
        //public async Task<ActionResult<BillListResponse>> GetBills([FromQuery] string consumerNumber)
        //{
        //    try
        //    {
        //        var bills = await _billService.GetByConsumerNumberAsync(consumerNumber);
        //        return Ok(new BillListResponse { bills = bills });
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Error fetching bills for consumerNumber {consumerNumber}", consumerNumber);
        //        return StatusCode(500, "Internal server error");
        //    }
        //}

        //[HttpGet("consumer/{consumerNumberId}")]
        //public async Task<ActionResult<BillPagedResponse>> GetBillsByConsumerNumberId(int consumerNumberId, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        //{
        //    try
        //    {
        //        var (items, totalCount) = await _billService.GetPagedByConsumerNumberIdAsync(consumerNumberId, page, pageSize);
        //        var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
        //        return Ok(new BillPagedResponse
        //        {
        //            items = items,
        //            totalCount = totalCount,
        //            page = page,
        //            pageSize = pageSize,
        //            totalPages = totalPages,
        //            hasNext = page < totalPages,
        //            hasPrevious = page > 1
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Error fetching bills for consumerNumberId {consumerNumberId}", consumerNumberId);
        //        return StatusCode(500, "Internal server error");
        //    }
        //}

        [HttpGet("{billId}")]
        public async Task<ActionResult<BillDto>> GetBill(int billId)
        {
            try
            {
                var bill = await _billService.GetByIdAsync(billId);
                if (bill == null) return NotFound();
                return Ok(bill);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching bill {billId}", billId);
                return StatusCode(500, "Internal server error");
            }
        }

        //[HttpPost]
        //public async Task<ActionResult<BillDto>> CreateBill([FromBody] CreateBillRequest request)
        //{
        //    try
        //    {
        //        var created = await _billService.CreateAsync(request);
        //        return CreatedAtAction(nameof(GetBill), new { billId = created.billId }, created);
        //    }
        //    catch (ArgumentException ex)
        //    {
        //        return BadRequest(new { message = ex.Message });
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Error creating bill");
        //        return StatusCode(500, "Internal server error");
        //    }
        //}

        //[HttpPost("{billId}/pay")]
        //public async Task<IActionResult> MarkPaid(int billId)
        //{
        //    try
        //    {
        //        var ok = await _billService.MarkPaidAsync(billId);
        //        if (!ok) return NotFound();
        //        return NoContent();
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Error marking bill {billId} paid", billId);
        //        return StatusCode(500, "Internal server error");
        //    }
        //}
    }
}

