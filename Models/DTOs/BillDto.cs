namespace SmartPayMobileApp_Backend.Models.DTOs
{
    public class BillDto
    {
        public int billId { get; set; }
        public string billName { get; set; } = string.Empty;
        public decimal amount { get; set; }
        public DateTime dueDate { get; set; }
        public DateTime expiryDate { get; set; }
        public bool isPaid { get; set; }
    }

    public class CreateBillRequest
    {
        public string consumerNumber { get; set; } = string.Empty;
        public string billName { get; set; } = string.Empty;
        public decimal amount { get; set; }
        public DateTime issueDate { get; set; }
        public DateTime dueDate { get; set; }
        public DateTime expiryDate { get; set; }
    }

    public class ConsumerNumberDto
    {
        public int consumerNumberId { get; set; }
        public string number { get; set; } = string.Empty;
    }

    public class RegisterConsumerNumberRequest
    {
        public int userId { get; set; }
        public string consumerNumber { get; set; } = string.Empty;
    }

    public class RegisterConsumerNumberResponse
    {
        public string message { get; set; } = string.Empty;
    }

    public class ConsumerNumberListResponse
    {
        public IEnumerable<ConsumerNumberDto> consumerNumbers { get; set; } = Enumerable.Empty<ConsumerNumberDto>();
    }

    public class BillListResponse
    {
        public IEnumerable<BillDto> bills { get; set; } = Enumerable.Empty<BillDto>();
    }

    public class PagedResponse<T>
    {
        public IEnumerable<T> items { get; set; } = Enumerable.Empty<T>();
        public int totalCount { get; set; }
        public int page { get; set; }
        public int pageSize { get; set; }
        public int totalPages { get; set; }
        public bool hasNext { get; set; }
        public bool hasPrevious { get; set; }
    }

    public class BillPagedResponse : PagedResponse<BillDto>
    {
    }
}

