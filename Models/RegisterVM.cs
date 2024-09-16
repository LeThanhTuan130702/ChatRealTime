using System.ComponentModel.DataAnnotations;

namespace ChatRealTime.Models
{
    public class RegisterVM
    {

        [Required]
        public string fname { get; set; } = null!;
        [Required]
        public string lname { get; set; } = null!;

        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]

        public string Password { get; set; } = null!;
        public string? Img { get; set; }



    }
}
