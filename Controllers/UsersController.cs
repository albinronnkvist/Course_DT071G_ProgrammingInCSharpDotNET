using AutoMapper;
using ForumAPI.Dtos.Post;
using ForumAPI.Dtos.User;
using ForumAPI.Repositories.UserRepository;
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
        public async Task<ActionResult<IEnumerable<GetUserDto>>> GetAllUsers()
        {
            return Ok();
        }

        // GET api/users/{id}
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<GetUserDto>> GetUserByID(int id)
        {
            return Ok();
        }
        
        // POST api/users/register
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<RegisterUserDto>> RegisterUser(RegisterUserDto req)
        {
            return Ok();
        }

        // POST api/users/login
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<LoginUserDto>> LoginUser(LoginUserDto req)
        {
            return Ok();
        }

        // PUT api/users/{id}
        [HttpPut]
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