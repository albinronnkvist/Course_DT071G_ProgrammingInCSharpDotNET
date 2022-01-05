using System.ComponentModel.DataAnnotations;

namespace ForumAPI.Models
{
    public class User
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public byte[] PasswordHash { get; set; }

        [Required]
        public byte[] PasswordSalt { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public List<Post> Posts { get; set; }
    }
}