using AutoMapper;
using ForumAPI.Dtos.Post;
using ForumAPI.Models;
using ForumAPI.Repositories.PostRepository;
using ForumAPI.UserSecurity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ForumAPI.Controllers
{
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
            var currentUserId = _token.GetUserId();

            if(currentUserId < 0)
            {
                return Unauthorized();
            }

            var newPost = _mapper.Map<Post>(req);
            newPost.UserId = currentUserId;
            await _postRepository.CreatePostAsync(newPost);
            await _postRepository.SaveChangesAsync();

            var res = _mapper.Map<GetPostDto>(newPost);

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
            if(currentUserId != post.UserId)
            {
                return Unauthorized();
            }

            _mapper.Map(req, post);
            _postRepository.UpdatePost(post);
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
            if(currentUserId != post.UserId)
            {
                return Unauthorized();
            }

            _postRepository.DeletePost(post);
            await _postRepository.SaveChangesAsync();

            return NoContent();
        }
    }
}