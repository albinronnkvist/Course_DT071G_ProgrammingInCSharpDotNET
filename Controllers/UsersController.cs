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
    [Authorize]
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {

        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UsersController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }


        // GET api/users
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetUserDto>>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllUsersAsync();

            var res = _mapper.Map<IEnumerable<GetUserDto>>(users);

            return Ok(res);
        }

        // GET api/users/{id}
        [AllowAnonymous]
        [HttpGet("{id}", Name="GetUserByIdAsync")]
        public async Task<ActionResult<GetUserDto>> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);

            if(user == null)
            {
                return NotFound();
            }
            
            var res = _mapper.Map<GetUserDto>(user);

            return Ok(res);
        }
        
        // POST api/users/register
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<RegisterUserDto>> RegisterUserAsync(RegisterUserDto req)
        {
            var newUser = _mapper.Map<User>(req);

            // Secure password
            byte[] salt = SecurePassword.GenerateRandomSalt();
            newUser.PasswordSalt = salt;
            newUser.PasswordHash = SecurePassword.SaltAndHashPassword(req.Password, salt);

            // Register and save to database
            await _userRepository.RegisterUserAsync(newUser);
            await _userRepository.SaveChangesAsync();

            // Map object to return
            var res = _mapper.Map<GetUserDto>(newUser);

            // 201 created
            return CreatedAtRoute(nameof(GetUserByIdAsync), new {Id = res.Id}, res);
        }

        // POST api/users/login
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<LoginUserDto>> LoginUserAsync(LoginUserDto req)
        {
            return Ok();
        }

        // PUT api/users/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUser(int id, UpdateUserDto req)
        {
            return Ok();
        }

        // DELETE api/users/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            return Ok();
        }
    }
}