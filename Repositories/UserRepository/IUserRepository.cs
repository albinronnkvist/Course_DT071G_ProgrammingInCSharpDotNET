using ForumAPI.Models;

namespace ForumAPI.Repositories.UserRepository
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(int id);
        Task<User> GetFullUserByIdAsync(int id);
        Task RegisterUserAsync(User user, string password);
        Task LoginUserAsync(string username, string password);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(User user);

        Task<bool> UserExistsAsync(string username);

        Task<bool> SaveChangesAsync();
    }
}