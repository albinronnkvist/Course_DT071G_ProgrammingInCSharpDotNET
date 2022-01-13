using AutoMapper;
using ForumAPI.Dtos.Post;
using ForumAPI.Dtos.User;
using ForumAPI.Models;
using ForumAPI.Repositories.UserRepository;
using ForumAPI.UserSecurity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ForumAPI.Controllers
{
    [Authorize] // Protect the routes with authorization by default.
    [ApiController] // Define that this class will be used to serve HTTP API responses.
    [Route("api/users")] // Define a route that clients can use to reach the functionality in this controller.
    public class UsersController : ControllerBase
    {
        
        // Inject interfaces to tell the framework to create instances of the implementation classes.
        // We have the UserRepository for accessing user data. 
        // We also have an instance of AutoMapper to copy data between object.
        // We also have an instance of the SecureToken class with functionality for creating a JWT and reading claims from it.
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly SecureToken _token;

        public UsersController(IUserRepository userRepository, IMapper mapper, SecureToken token)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _token = token;
        }


        // GET api/users
        // Get all users.
        [AllowAnonymous] // Allow all clients to access this endpoint
        [HttpGet] // Use the HTTP-GET method to access this endpoint
        public async Task<ActionResult<IEnumerable<GetUserDto>>> GetAllUsersAsync()
        {
            // Get a list of User objects from the databse via the UserRepository.
            var users = await _userRepository.GetAllUsersAsync();
            
            // Map the full User objects to lean GetUserDto objects. 
            var res = _mapper.Map<IEnumerable<GetUserDto>>(users);

            // Return the list of users with the status code 200, successful request.
            return Ok(res);
        }

        // GET api/users/{id}
        // Get a single user by id.
        [AllowAnonymous]
        [HttpGet("{id}", Name="GetUserByIdAsync")] // Pass an id to reach this endpoint
        public async Task<ActionResult<GetUserDto>> GetUserByIdAsync(int id) // Get the id from the request
        {
            // Pass the id into a UserRepository method that returns a User object from the database that matches the id.
            // If no matches were made (no user was found), then the method returns null.
            var user = await _userRepository.GetUserByIdAsync(id);

            // If no user in the database matched the id
            if(user == null)
            {
                // Return status code 404, not found.
                return NotFound();
            }
            
            // Otherwise, if a user was found. Map the full User object from the database to a lean GetUserDto object.
            var res = _mapper.Map<GetUserDto>(user);
            
            // And return the object to the client with the status code 200.
            return Ok(res);
        }

        // GET api/users/full/{id}
        // Get a single user by id, but with more information.
        [HttpGet("full/{id}", Name="GetFullUserByIdAsync")]
        public async Task<ActionResult<GetFullUserDto>> GetFullUserByIdAsync(int id)
        {
            // Get a user from the database with the id that was passed in the request.
            var user = await _userRepository.GetUserByIdAsync(id);

            if(user == null)
            {
                return NotFound();
            }

            // Use the GetUserId method the get the id from the request JWT
            var currentUserId = _token.GetUserId();

            // If the user from the database does NOT have the same id as the id in the JWT
            if(user.Id != currentUserId)
            {
                // Return status code 401, not authorized (access denied).
                return Unauthorized();
            }
            
            // Otherwise, if the id:s match. 
            // Map the data to a DTO object and return it with the status code 200.
            var res = _mapper.Map<GetFullUserDto>(user);
            return Ok(res);
        }
        
        // POST api/users/register
        // Register a user.
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<RegisterUserDto>> RegisterUserAsync(RegisterUserDto req) // Recieve a DTO from the request with username, email and password.
        {
            // Check if username and email already exist in the database
            var usernameExists = await _userRepository.UsernameExistsAsync(req.Username);
            var emailExists = await _userRepository.EmailExistsAsync(req.Email);

            // If they already exist
            if(usernameExists || emailExists)
            {
                // Return status code 409, conflict.
                return Conflict();
            }

            // Map DTO object from the request to a full User object
            var newUser = _mapper.Map<User>(req);

            // Generate salt and a hashed password and add to the new mapped User object
            byte[] salt = SecurePassword.GenerateRandomSalt();
            newUser.PasswordSalt = salt;
            newUser.PasswordHash = SecurePassword.SaltAndHashPassword(req.Password, salt);

            // Register the new user and save it to the database
            await _userRepository.RegisterUserAsync(newUser);
            await _userRepository.SaveChangesAsync();

            // Map the User object to a DTO
            var res = _mapper.Map<GetUserDto>(newUser);

            // Return the DTO with the status code 201, created. 
            // Also pass the route to reach the created user and a body with the users information.
            return CreatedAtRoute(nameof(GetUserByIdAsync), new {Id = res.Id}, res);
        }

        // POST api/users/login
        // Authenticate a user.
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<LoginUserDto>> LoginUserAsync(LoginUserDto req) // Recieve a DTO from the request with username and password.
        {
            // Check if a user exists in the database that has the same username as the one that was sent in the request. 
            var user = await _userRepository.LoginUserAsync(req.Username);
            
            // If no user with that username was found.
            if(user == null)
            {
                // Return status code 401, unauthorized (wrong username or password, try again).
                return Unauthorized();
            }

            // Otherwise, if a user was found.
            // Verify the password sent in the request with the password hash that is stored in the database.
            var verifiedPassword = SecurePassword.VerifyPasswordHash(req.Password, user.PasswordHash, user.PasswordSalt);

            // If the passwords do no match.
            if(!verifiedPassword)
            {
                // Return status code 401, unauthorized (wrong username or password, try again).
                return Unauthorized();
            }

            // Otherwise, if the passwords match.
            // Create a JWT with the CreateToken() in the SecurityToken class and pass the User object as a parameter.
            var token = _token.CreateToken(user);

            // Return the JWT to the client with the status code 200.
            return Ok(token);
        }

        // PUT api/users/username{id}
        // Update a user.
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUserAsync(int id, UpdateUserDto req)
        {
            // Get user with the request id
            var user = await _userRepository.GetUserByIdAsync(id);

            // If no user was found in the database
            if(user == null)
            {
                // 404 not found
                return NotFound();
            }

            // Get current user id from token
            var currentUserId = _token.GetUserId();

            // If user tries to update another user
            if(user.Id != currentUserId)
            {
                // 401 unauthorized
                return Unauthorized();
            }

            // Check if username and email already exists
            var usernameExists = await _userRepository.UsernameExistsAsync(req.Username);
            var emailExists = await _userRepository.EmailExistsAsync(req.Email);

            // If they already exist
            if(usernameExists)
            {
                if(!req.Username.ToLower().Equals(user.Username.ToLower()))
                {
                    // 409 conflict
                    return Conflict();
                } 
            }

            if(emailExists)
            {
                if(!req.Email.ToLower().Equals(user.Email.ToLower()))
                {
                    // 409 conflict
                    return Conflict();
                }
            }
            
            // Replace email and password
            user.Username = req.Username;
            user.Email = req.Email;

            // Replace password hash and salt
            byte[] salt = SecurePassword.GenerateRandomSalt();
            user.PasswordSalt = salt;
            user.PasswordHash = SecurePassword.SaltAndHashPassword(req.Password, salt);
            
            // Save changes
            await _userRepository.SaveChangesAsync();

            // 204 no content. Indicates that a request has succeeded, but that the client doesn't need to navigate away from its current page. 
            return NoContent();
        }

        // PATCH api/users/{id}
        // Not implemented yet

        // DELETE api/users/{id}
        // Delete a user.
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUserAsync(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if(user == null)
            {
                return NotFound();
            }

            var currentUserId = _token.GetUserId();
            if(user.Id != currentUserId)
            {
                return Unauthorized();
            }

            _userRepository.DeleteUser(user);
            await _userRepository.SaveChangesAsync();

            return NoContent();
        }
    }
}