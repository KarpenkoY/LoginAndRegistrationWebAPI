using System.ComponentModel.DataAnnotations;

namespace LoginAndRegistrationWebAPI.Models
{
    public class User
    {
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
