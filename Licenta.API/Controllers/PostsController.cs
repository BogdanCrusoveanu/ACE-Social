using Licenta.API.Data;
using Licenta.API.Models;
using Licenta.API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Licenta.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostsService _postsService;
        private readonly IGenericsRepository _genericsRepo;

        public PostsController(IPostsService postsService, IGenericsRepository genericsRepo)
        {
            _postsService = postsService;
            _genericsRepo = genericsRepo;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddPost(Post post)
        {
            _postsService.AddPost(post);

            if (await _genericsRepo.SaveAll())
            {
                return NoContent();
            }

            return BadRequest("Something went wrong!");
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetAllPosts()
        {
            var posts = await _postsService.GetPosts();

            var mappedPosts = _postsService.MapPosts(posts);

            return Ok(mappedPosts);
        }


        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetPostsForUser(int id)
        {
            var posts = await _postsService.GetUserPosts(id);

            var mappedPosts = _postsService.MapPosts(posts);

            return Ok(mappedPosts);
        }

        [HttpGet("get/commented/{id}")]
        public async Task<IActionResult> GetPostsUserCommented(int id)
        {
            var posts = await _postsService.GetPostsUserCommented(id);

            var mappedPosts = _postsService.MapPosts(posts);

            return Ok(mappedPosts);
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdatePost(Post post)
        {
            var updatedPost = await _postsService.UpdatePost(post);

            if (await _genericsRepo.SaveAll())
            {
                return NoContent();
            }

            return BadRequest("No changes were made");
        }

        [HttpPost("delete")]
        public async Task<IActionResult> DeletePost(Post post)
        {
            if (post.UserId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }
            _genericsRepo.Delete(post);

            if (await _genericsRepo.SaveAll())
            {
                return NoContent();
            }
            return BadRequest("Delete Failed!");
        }
    }
}