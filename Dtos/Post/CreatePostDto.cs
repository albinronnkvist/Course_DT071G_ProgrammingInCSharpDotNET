using System.ComponentModel.DataAnnotations;

namespace ForumAPI.Dtos.Post
{
    // DTO for creating a post. The client should only be able to access the Title and Text properties.
    public class CreatePostDto
    {
        [Required]
        [MaxLength(150)]
        public string Title { get; set; }

        [Required]
        public string Text { get; set; }
    }
}