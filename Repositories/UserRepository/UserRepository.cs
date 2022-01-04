using ForumAPI.Models;

namespace ForumAPI.Repositories.UserRepository
{
  public class UserRepository : IUserRepository
  {
    public Task<IEnumerable<User>> GetAllUsersAsync()
    {
      throw new NotImplementedException();
    }

    public Task<User> GetUserByIdAsync(int id)
    {
      throw new NotImplementedException();
    }

    public Task<User> GetFullUserByIdAsync(int id)
    {
      throw new NotImplementedException();
    }

    public Task RegisterUserAsync(User user, string password)
    {
      throw new NotImplementedException();
    }

    public Task LoginUserAsync(string username, string password)
    {
      throw new NotImplementedException();
    }

    public Task UpdateUserAsync(User user)
    {
      throw new NotImplementedException();
    }
    
    public Task DeleteUserAsync(User user)
    {
      throw new NotImplementedException();
    }



    public Task<bool> UserExistsAsync(string username)
    {
      throw new NotImplementedException();
    }



    public Task<bool> SaveChangesAsync()
    {
      throw new NotImplementedException();
    }
  }
}