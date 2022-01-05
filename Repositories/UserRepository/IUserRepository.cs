using ForumAPI.Models;

namespace ForumAPI.Repositories.UserRepository
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(int id);
        Task RegisterUserAsync(User user);
        Task<User> LoginUserAsync(string username);
        void UpdateUser(User user);
        void DeleteUser(User user);

        Task<bool> UsernameExistsAsync(string username);
        Task<bool> EmailExistsAsync(string email);

        Task<bool> SaveChangesAsync();
    }
}