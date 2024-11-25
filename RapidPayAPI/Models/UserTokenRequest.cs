using System.ComponentModel.DataAnnotations;

namespace RapidPayAPI.Models
{
    public class UserTokenRequest
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

    }
}
