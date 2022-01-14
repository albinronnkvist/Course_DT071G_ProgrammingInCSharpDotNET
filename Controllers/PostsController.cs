using AutoMapper;
using ForumAPI.Dtos.Post;
using ForumAPI.Models;
using ForumAPI.Repositories.PostRepository;
using ForumAPI.UserSecurity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ForumAPI.Controllers
{
    // Implemented in the same way as the UsersController.
    [Authorize]
    [ApiController]
    [Route("api/posts")]
    public class PostsController : ControllerBase
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;
        private readonly SecureToken _token;

        public PostsController(IPostRepository postRepository, IMapper mapper, SecureToken token)
        {
            _postRepository = postRepository;
            _mapper = mapper;
            _token = token;
        }


        // GET api/posts
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetPostDto>>> GetAllPostsAsync()
        {
            var posts = await _postRepository.GetAllPostsAsync();

            var res = _mapper.Map<IEnumerable<GetPostDto>>(posts);
            return Ok(res);
        }

        // GET api/posts/{id}
        [AllowAnonymous]
        [HttpGet("{id}", Name="GetPostByIdAsync")]
        public async Task<ActionResult<GetPostDto>> GetPostByIdAsync(int id)
        {
            var post = await _postRepository.GetPostByIdAsync(id);

            if(post == null)
            {
                return NotFound();
            }

            var res = _mapper.Map<GetPostDto>(post);

            return Ok(res);
        }
        
        // POST api/posts
        [HttpPost]
        public async Task<ActionResult<CreatePostDto>> CreatePostAsync(CreatePostDto req)
        {
            // Get the id from a claim in the JWT passed with the request.
            var currentUserId = _token.GetUserId();

            // If the JWT does not have an id or the id is incorrect.
            if(currentUserId < 0)
            {
                // Return 401, unauthorized.
                return Unauthorized();
            }
            
            // Map the CreatePostDto to a full Post object.
            var newPost = _mapper.Map<Post>(req);

            // Add a user id as the foreign key. This id was fetched with the GetUserId() method above.
            newPost.UserId = currentUserId;

            // Track the post to be created.
            await _postRepository.CreatePostAsync(newPost);

            // Save changes to insert the tracked post into the database.
            await _postRepository.SaveChangesAsync();

            // Map the new full post object to a GetPostDto object.
            var res = _mapper.Map<GetPostDto>(newPost);

            // Return status code 201, created. 
            // Also pass the route to reach the created post and a body with the users information.
            return CreatedAtRoute(nameof(GetPostByIdAsync), new {Id = res.Id}, res);
        }

        // PUT api/posts/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePostAsync(int id, UpdatePostDto req)
        {
            var post = await _postRepository.GetPostByIdAsync(id);
            if(post == null)
            {
                return NotFound();
            }

            var currentUserId = _token.GetUserId();

            // If a user tries to edit another users post.
            if(currentUserId != post.UserId)
            {
                // 401, unauthorized.
                return Unauthorized();
            }

            _mapper.Map(req, post);
            await _postRepository.SaveChangesAsync();    

            return NoContent();
        }

        // DELETE api/posts/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePostAsync(int id)
        {
            var post = await _postRepository.GetPostByIdAsync(id);
            if(post == null)
            {
                return NotFound();
            }

            var currentUserId = _token.GetUserId();

            // If a user tries to delete another users post.
            if(currentUserId != post.UserId)
            {
                // 401, unauthorized.
                return Unauthorized();
            }

            _postRepository.DeletePost(post);
            await _postRepository.SaveChangesAsync();

            return NoContent();
        }
    }
}