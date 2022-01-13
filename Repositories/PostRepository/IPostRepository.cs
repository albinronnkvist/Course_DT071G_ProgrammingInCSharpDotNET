using ForumAPI.Models;

namespace ForumAPI.Repositories.PostRepository
{
    // Implemented in the same way as IUserRepository, go to that file for more information.
    public interface IPostRepository
    {
        Task<IEnumerable<Post>> GetAllPostsAsync();
        Task<Post> GetPostByIdAsync(int id);
        Task CreatePostAsync(Post post);
        void DeletePost(Post post);
        
        Task<bool> SaveChangesAsync();
    }
}