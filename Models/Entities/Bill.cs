using System.ComponentModel.DataAnnotations;

namespace SmartPayMobileApp_Backend.Models.Entities
{
    public class Bill
    {
        public int BillId { get; set; }

        [Required]
        [MaxLength(100)]
        public string BillName { get; set; } = string.Empty;

        [Range(0, double.MaxValue)]
        public decimal Amount { get; set; }

        public DateTime IssueDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool IsPaid { get; set; }

        public int ConsumerNumberId { get; set; }
        public ConsumerNumber? ConsumerNumber { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}

