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
    public class CommentsController : ControllerBase
    {
        private readonly ICommentsService _commentsService;
        private readonly IGenericsRepository _genericsRepo;

        public CommentsController(ICommentsService commentsService, IGenericsRepository genericsRepo)
        {
            _commentsService = commentsService;
            _genericsRepo = genericsRepo;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddComment(Comment comment)
        {
            if (comment.Content != "")
            {
                _commentsService.AddComment(comment);
            } else
            {
                return BadRequest("Nu poți posta comentarii goale!");
            }

            if (await _genericsRepo.SaveAll())
            {
                return NoContent();
            }

            return BadRequest("Something went wrong!");
        }

        //[HttpGet("get/{postId}")]
        //public async Task<IActionResult> GetAllComments(postId)
        //{
        //    var comments = await _commentsService.GetAllComments(postId);

        //    var mappedComments = _postsService.MapPostsForReturn(comments);

        //    return Ok(mappedComments);
        //}


        [HttpPost("update")]
        public async Task<IActionResult> UpdateComment(Comment comment)
        {
            var updatedComment = await _commentsService.UpdateComment(comment);

            if (await _genericsRepo.SaveAll())
            {
                return NoContent();
            }

            return BadRequest("No changes were made");
        }

        [HttpPost("delete")]
        public async Task<IActionResult> DeleteComment(Comment comment)
        {
            if (comment.UserId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            _genericsRepo.Delete(comment);

            if (await _genericsRepo.SaveAll())
            {
                return NoContent();
            }
            return BadRequest("Delete Failed!");
        }
    }
}