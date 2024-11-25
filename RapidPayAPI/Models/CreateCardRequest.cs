using System.ComponentModel.DataAnnotations;

namespace RapidPayAPI.Models
{
    public class CreateCardRequest
    {
        [Required]
        [MaxLength(15)]
        public string Number { get; set; }
        [Required]
        [MaxLength(2)]
        public string ExpiryMonth { get; set; }
        [Required]
        [MaxLength(4)]
        public string ExpiryYear { get; set; }
        [Required]
        [MaxLength(4)]
        public string Cvc { get; set; }
        [Required]
        [MaxLength(255)]
        public string HolderName { get; set; }
        [Required]
        public decimal Balance { get; set; }
    }
}
