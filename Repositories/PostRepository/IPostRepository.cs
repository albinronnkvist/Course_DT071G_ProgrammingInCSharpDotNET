using ForumAPI.Models;

namespace ForumAPI.Repositories.PostRepository
{
    public interface IPostRepository
    {
        Task<IEnumerable<Post>> GetAllPostsAsync();
        Task<Post> GetPostByIdAsync(int id);
        Task CreatePostAsync(Post post);
        void UpdatePostAsync(Post post);
        void DeletePostAsync(Post post);
        
        Task<bool> SaveChangesAsync();
    }
}