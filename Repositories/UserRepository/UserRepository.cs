using ForumAPI.Data;
using ForumAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ForumAPI.Repositories.UserRepository
{
  public class UserRepository : IUserRepository
  {
    private readonly DataContext _context;

    public UserRepository(DataContext context)
    {
      _context = context;
    }



    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
      var users = await _context.Users.ToListAsync();
      return users;
    }

    public async Task<User> GetUserByIdAsync(int id)
    {
      var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
      return user;
    }

    public async Task RegisterUserAsync(User user)
    {
      if(user == null)
      {
        throw new ArgumentNullException(nameof(user));
      }

      await _context.Users.AddAsync(user);
    }

    public async Task<User> LoginUserAsync(string username)
    {
      if(string.IsNullOrEmpty(username) || string.IsNullOrWhiteSpace(username))
      {
        throw new ArgumentNullException(nameof(username));
      }

      var user = await _context.Users.FirstOrDefaultAsync(u => u.Username.ToLower() == username.ToLower());
      
      return user;
    }

    public void UpdateUser(User user)
    {
      // Nothing
    }
    
    public void DeleteUser(User user)
    {
      if(user == null)
      {
        throw new ArgumentNullException(nameof(user));
      }

      _context.Users.Remove(user);
    }



    public async Task<bool> UsernameExistsAsync(string username)
    {
      if(string.IsNullOrEmpty(username) || string.IsNullOrWhiteSpace(username))
      {
        throw new ArgumentNullException(nameof(username));
      }
      
      return await _context.Users.AnyAsync(u => u.Username.ToLower().Equals(username.ToLower()));
    }

    public async Task<bool> EmailExistsAsync(string email)
    {
      if(string.IsNullOrEmpty(email) || string.IsNullOrWhiteSpace(email))
      {
        throw new ArgumentNullException(nameof(email));
      }
      
      return await _context.Users.AnyAsync(u => u.Email.ToLower().Equals(email.ToLower()));
    }



    public async Task<bool> SaveChangesAsync()
    {
      return (await _context.SaveChangesAsync() >= 0);
    }
  }
}