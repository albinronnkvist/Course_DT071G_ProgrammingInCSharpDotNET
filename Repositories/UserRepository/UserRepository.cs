using ForumAPI.Data;
using ForumAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ForumAPI.Repositories.UserRepository
{
  // Implement the IUserRepository with all the members.
  public class UserRepository : IUserRepository
  {
    // Inject an instance of the DbContext class from EF Core that is used to access the database.
    // Store it in a private field to be able to access it in the class.
    private readonly DataContext _context;

    public UserRepository(DataContext context)
    {
      _context = context;
    }


    // Get all users.
    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
      // Use the DbContext class to access the Users table in the database and return all users as a list.
      // Await the response of the method and then return the response, which is a list of User objects.
      var users = await _context.Users.ToListAsync();
      return users;
    }

    // Get a single user by id.
    // Pass an id as a parameter in the method.
    public async Task<User> GetUserByIdAsync(int id)
    {
      // Get a user from the Users table that has an id which matches the input id.
      var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

      // Return a User object if there was a match, or null if there were no matches.
      return user;
    }

    // Register a user.
    // Pass a User object.
    public async Task RegisterUserAsync(User user)
    {
      // Check if the object has a value. If it's null, throw an exception.
      if(user == null)
      {
        throw new ArgumentNullException(nameof(user));
      }

      // Begin tracking the new User entity which will be inserted when the SaveChangesAsync() method is called.
      await _context.Users.AddAsync(user);
    }

    // Login user.
    // Pass a username.
    public async Task<User> LoginUserAsync(string username)
    {
      // Check if the input username has an accepted value. If not, throw an exception,
      if(string.IsNullOrEmpty(username) || string.IsNullOrWhiteSpace(username))
      {
        throw new ArgumentNullException(nameof(username));
      }

      // Get the first user from the Users table that matches the input username.
      var user = await _context.Users.FirstOrDefaultAsync(u => u.Username.ToLower() == username.ToLower());
      
      // Return the User object if there was a match or null if there were no matches.
      return user;
    }

    // Update a user.
    public void UpdateUser(User user)
    {
      // Nothing
    }
    
    // Delete a user.
    // Pass a User object
    public void DeleteUser(User user)
    {
      // Check if the User object has a value. If it's null, throw an exception.
      if(user == null)
      {
        throw new ArgumentNullException(nameof(user));
      }

      // Otherwise, begin tracking the User entity that will be deleted when the SaveChangesAsync() method is called. 
      _context.Users.Remove(user);
    }


    // Check if a username exists.
    // Pass a username.
    public async Task<bool> UsernameExistsAsync(string username)
    {
      if(string.IsNullOrEmpty(username) || string.IsNullOrWhiteSpace(username))
      {
        throw new ArgumentNullException(nameof(username));
      }
      
      // Return true if the input username exists in the database or false if it does not.
      return await _context.Users.AnyAsync(u => u.Username.ToLower().Equals(username.ToLower()));
    }

    // Check if an email exists, in the same way as the method above.
    public async Task<bool> EmailExistsAsync(string email)
    {
      if(string.IsNullOrEmpty(email) || string.IsNullOrWhiteSpace(email))
      {
        throw new ArgumentNullException(nameof(email));
      }
      
      return await _context.Users.AnyAsync(u => u.Email.ToLower().Equals(email.ToLower()));
    }


    // Save changes to the database.
    public async Task<bool> SaveChangesAsync()
    {
      // SaveChangesAsync() saves all changes made in the context to the database. 
      // So when create, update or delete a user, the changes won't be reflected in the database until we call this method.
      return (await _context.SaveChangesAsync() >= 0);
    }
  }
}