using Licenta.API.Data;
using Licenta.API.Dtos;
using Licenta.API.Services;
using Licenta.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Licenta.Controllers
{
    [ServiceFilter(typeof(LogUserActivity))]
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _usersService;
        private readonly IGenericsRepository _genericsRepo;

        public UsersController(IUserService userService, IGenericsRepository genericsRepo)
        {
            _usersService = userService;
            _genericsRepo = genericsRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers([FromQuery]UserParams userParams)
        {
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            userParams.UserId = currentUserId;

            var users = await _usersService.GetUsers(userParams);

            var usersToReturn = _usersService.MapUsersToReturn(users, currentUserId);

            usersToReturn = _usersService.AddCategories(usersToReturn);

            Response.AddPagination(users.CurrentPage, users.PageSize,
                users.TotalCount, users.TotalPages);

            return Ok(usersToReturn);
        }

        [HttpGet("{id}", Name = "GetUser")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _usersService.GetUser(id);

            var userToReturn = _usersService.MapUserForReturn(user);

            return Ok(userToReturn);
        }

        [HttpPost("{userId}/like/{recipientId}")]
        public async Task<IActionResult> LikeUser(int userId, int recipientId)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var like = await _usersService.GetLike(userId, recipientId);

            if (like != null)
            {
                _genericsRepo.Delete(like);
            }

            if (await _genericsRepo.SaveAll())
            {
                return BadRequest("Ai șters utilizatorul din lista de prieteni!");
            }

            if (await _usersService.GetUser(recipientId) == null)
                return NotFound();

            _usersService.AddLike(userId, recipientId, like);

            if(await _genericsRepo.SaveAll())
            {
                return Ok();
            }

            return BadRequest("Failed to like user!");
        }

        [HttpGet("GetUsersFromSpecialization/{specializationId}")]
        public async Task<IActionResult> GetUsersFromSpecialization(int specializationId, [FromQuery] UserParams userParams)
        {
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            userParams.UserId = currentUserId;

            var usersFormSpecialization = await _usersService.GetUsersFromSpecialization(specializationId, userParams);

            var usersToReturn = _usersService.MapUsersFromCategory(usersFormSpecialization);

            return Ok(usersToReturn);
        }

        [HttpGet("GetUsersFromGroup/{groupId}")]
        public async Task<IActionResult> GetUsersFromGroup(int groupId, [FromQuery] UserParams userParams)
        {
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            userParams.UserId = currentUserId;

            var usersFromGroup = await _usersService.GetUsersFromGroup(groupId, userParams);

            var usersToReturn = _usersService.MapUsersFromCategory(usersFromGroup);

            return Ok(usersToReturn);
        }

        [HttpGet("GetUsersFromSubGroup/{subGroupId}")]
        public async Task<IActionResult> GetUsersFromSubGroup(int subGroupId, [FromQuery] UserParams userParams)
        {
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            userParams.UserId = currentUserId;

            var usersFromSubGroup = await _usersService.GetUsersFromSubGroup(subGroupId, userParams);

            var usersToReturn = _usersService.MapUsersFromCategory(usersFromSubGroup);

            return Ok(usersToReturn);
        }

        [HttpPost("UpdateUser/{id}")]
        public async Task<IActionResult> UpdateUser(UserForUpdateDto userForUpdate, int id)
        {
            var updatedUser = await _usersService.UpdateUser(userForUpdate, id);

            if (await _genericsRepo.SaveAll())
            {
                return Ok(updatedUser);
            }
            return BadRequest("Update Failed or the same user informations were sent");
        }
    }
}