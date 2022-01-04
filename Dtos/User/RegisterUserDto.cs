using System.ComponentModel.DataAnnotations;

namespace ForumAPI.Dtos.User
{
    public class RegisterUserDto : LoginUserDto
    {
        [Required]
        public string? Email { get; set; }
    }
}