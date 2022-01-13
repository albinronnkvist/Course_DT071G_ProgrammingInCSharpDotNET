using ForumAPI.Data;
using ForumAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ForumAPI.Repositories.PostRepository
{
  // Works the same way as UserRepository, go to that file for more information.
  public class PostRepository : IPostRepository
  {
    private readonly DataContext _context;

    public PostRepository(DataContext context)
    {
      _context = context;
    }



    public async Task<IEnumerable<Post>> GetAllPostsAsync()
    {
      var posts = await _context.Posts.ToListAsync();
      return posts;
    }

    public async Task<Post> GetPostByIdAsync(int id)
    {
      var post = await _context.Posts.FirstOrDefaultAsync(p => p.Id == id);
      
      return post;
    }

    public async Task CreatePostAsync(Post post)
    {
      if(post == null)
      {
        throw new ArgumentNullException(nameof(post));
      }

      await _context.Posts.AddAsync(post);
    }

    public void DeletePost(Post post)
    {
      if(post == null)
      {
        throw new ArgumentNullException(nameof(post));
      }

      _context.Posts.Remove(post);
    }

    

    public async Task<bool> SaveChangesAsync()
    {
      return (await _context.SaveChangesAsync() >= 0);
    }
  }
}