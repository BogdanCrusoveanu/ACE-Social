using Licenta.API.Models;
using Licenta.API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Licenta.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly IGroupsService _groupsService;

        public GroupsController(IGroupsService groupsService)
        {
            _groupsService = groupsService;
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetGroups()
        {
            var groups = await _groupsService.GetGroups();

            var groupsToReturn = _groupsService.MapGroups(groups);

            return Ok(groupsToReturn);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddGroup(Group group)
        {
            if (await _groupsService.GroupExists(group))
            {
                return BadRequest("The group you entered already exists!");
            }

            _groupsService.AddGroup(group);

            if (await _groupsService.SaveChangesInContext())
            {
                return NoContent();
            }

            return BadRequest("Something went wrong!");
        }


        [HttpPost("update")]
        public async Task<IActionResult> UpdateGroup(Group group)
        {
            var updatedGroup = await _groupsService.UpdateGroup(group);

            if (await _groupsService.SaveChangesInContext())
            {
                return Ok(updatedGroup);
            }
            return BadRequest("Update Failed or the same group was sent");
        }

    }
}