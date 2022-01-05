using System.ComponentModel.DataAnnotations;

namespace ForumAPI.Models
{
    public class Post
    {
        [Key]
        [Required]
        public int Id { get; set;}

        [Required]
        [MaxLength(150)]
        public string Title { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Required]
        public int UserId { get; set; }

        [Required]
        public User User { get; set; }
    }
}