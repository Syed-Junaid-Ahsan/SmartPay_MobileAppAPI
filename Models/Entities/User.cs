using System.ComponentModel.DataAnnotations;

namespace SmartPayMobileApp_Backend.Models.Entities
{
    public class User
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;
        
        [Required]
        [EmailAddress]
        [MaxLength(50)]
        public string Email { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(15)]
        public string PhoneNumber { get; set; } = string.Empty;
        
        // Store only the password hash in the database (no plain password)
        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        [Required]
        [MaxLength(13)]
        public string CnicNumber { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(100)]
        public string DeviceId { get; set; } = string.Empty;
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; } = true;

        public ICollection<ConsumerNumber> ConsumerNumbers { get; set; } = new List<ConsumerNumber>();
    }
}
