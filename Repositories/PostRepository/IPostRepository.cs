using ForumAPI.Models;

namespace ForumAPI.Repositories.PostRepository
{
    public interface IPostRepository
    {
        Task<IEnumerable<Post>> GetAllPostsAsync();
        Task<Post> GetPostByIdAsync(int id);
        Task CreatePostAsync(Post post);
        void UpdatePost(Post post);
        void DeletePost(Post post);
        
        Task<bool> SaveChangesAsync();
    }
}