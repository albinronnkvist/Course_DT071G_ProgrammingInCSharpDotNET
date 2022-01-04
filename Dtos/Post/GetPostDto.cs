using ForumAPI.Dtos.User;

namespace ForumAPI.Dtos.Post
{
    public class GetPostDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Text { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? UserId { get; set; }
    }
}