using ForumAPI.Models;

namespace ForumAPI.Repositories.PostRepository
{
    public interface IPostRepository
    {
        Task<IEnumerable<Post>> GetAllPostsAsync();
        Task<Post> GetPostByIdAsync(int id);
        Task CreatePostAsync(Post post);
        Task UpdatePostAsync(Post post);
        Task DeletePostAsync(Post post);
        
        Task<bool> SaveChangesAsync();
    }
}