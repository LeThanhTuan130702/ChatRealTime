using System.ComponentModel.DataAnnotations;

namespace ChatRealTime.Models
{
    public class LoginVM
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        public string PasswordHash { get; set; } = null!;

    }
}
