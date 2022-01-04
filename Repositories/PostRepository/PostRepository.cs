using ForumAPI.Data;
using ForumAPI.Models;

namespace ForumAPI.Repositories.PostRepository
{
  public class PostRepository : IPostRepository
  {
    private readonly DataContext _context;

    public PostRepository(DataContext context)
    {
      _context = context;
    }



    public Task<IEnumerable<Post>> GetAllPostsAsync()
    {
      throw new NotImplementedException();
    }

    public Task<Post> GetPostByIdAsync(int id)
    {
      throw new NotImplementedException();
    }

    public Task CreatePostAsync(Post post)
    {
      throw new NotImplementedException();
    }

    public Task UpdatePostAsync(Post post)
    {
      throw new NotImplementedException();
    }

    public Task DeletePostAsync(Post post)
    {
      throw new NotImplementedException();
    }

    

    public Task<bool> SaveChangesAsync()
    {
      throw new NotImplementedException();
    }
  }
}