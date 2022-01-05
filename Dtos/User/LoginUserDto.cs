using System.ComponentModel.DataAnnotations;

namespace ForumAPI.Dtos.User
{
    public class LoginUserDto
    {
        [Required]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}