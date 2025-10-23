using System.ComponentModel.DataAnnotations;

namespace SmartPayMobileApp_Backend.Models.Entities
{
    public class Bill
    {
        public int BillId { get; set; }
        public string BillName { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool IsPaid { get; set; }
        public int ConsumerNumberId { get; set; }

        // Navigation properties (optional)
        public ConsumerNumber? ConsumerNumber { get; set; }
    }
}

