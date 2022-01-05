using System.ComponentModel.DataAnnotations;

namespace ForumAPI.Dtos.Post
{
    public class CreatePostDto
    {
        [Required]
        [MaxLength(150)]
        public string Title { get; set; }

        [Required]
        public string Text { get; set; }
    }
}