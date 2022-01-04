namespace ForumAPI.Dtos.User
{
    public class GetUserDto
    {
        public int Id { get; set; }

        public string? Username { get; set; }

        public string? Email { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}