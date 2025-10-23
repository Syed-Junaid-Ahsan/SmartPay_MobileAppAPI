using System.ComponentModel.DataAnnotations;

namespace SmartPayMobileApp_Backend.Models.Entities
{
    public class ConsumerNumber
    {
        public int ConsumerNumberId { get; set; }

        [Required]
        [MaxLength(24)]
        public string Number { get; set; } = string.Empty;

        public int CustomerUserId { get; set; }
        public User? User { get; set; }

        public ICollection<Bill> Bills { get; set; } = new List<Bill>();
    }
}



