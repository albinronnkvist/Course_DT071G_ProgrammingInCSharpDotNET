using ForumAPI.Dtos.Post;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ForumAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/posts")]
    public class PostsController : ControllerBase
    {
        // GET api/posts
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetPostDto>>> GetAllPosts()
        {
            return Ok();
        }

        // GET api/posts/{id}
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<GetPostDto>> GetPostByID(int id)
        {
            return Ok();
        }
        
        // POST api/posts
        [HttpPost]
        public async Task<ActionResult<CreatePostDto>> CreatePost(CreatePostDto req)
        {
            return Ok();
        }

        // PUT api/posts/{id}
        [HttpPut]
        public async Task<ActionResult> UpdatePost(int id, UpdatePostDto req)
        {
            return Ok();
        }

        // DELETE api/posts/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePost(int id)
        {
            return Ok();
        }
    }
}