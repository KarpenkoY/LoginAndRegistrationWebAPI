using System.ComponentModel.DataAnnotations;

namespace LoginAndRegistrationWebAPI.Models
{
    public class User
    {
        [Required]
        [StringLength(32, MinimumLength = 5)]
        public string Login { get; set; }

        [Required]
        [StringLength(32, MinimumLength = 5)]
        public string Password { get; set; }
    }
}
