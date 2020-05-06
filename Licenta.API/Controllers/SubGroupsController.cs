using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Licenta.API.Data;
using Licenta.API.Models;
using Licenta.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Licenta.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SubGroupsController : ControllerBase
    {
        private readonly ISubGroupsService _subGroupsService;
        private readonly IGenericsRepository _genericsRepo;

        public SubGroupsController(ISubGroupsService subGroupsService, IGenericsRepository genericsRepo)
        {
            _subGroupsService = subGroupsService;
            _genericsRepo = genericsRepo;
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetSubGroups()
        {
            var subGroups = await _subGroupsService.GetSubGroups();

            var subGroupsToReturn = _subGroupsService.MapSubGroups(subGroups);

            return Ok(subGroupsToReturn);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddSubGroup(SubGroup subGroup)
        {
            if (await _subGroupsService.SubGroupExists(subGroup))
            {
                return BadRequest("The subgroup you entered already exists!");
            }

            _subGroupsService.AddSubGroup(subGroup);

            if (await _subGroupsService.SaveChangesInContext())
            {
                return NoContent();
            }

            return BadRequest("Something went wrong!");
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateSubGroup(SubGroup subGroup)
        {
            var updatedSubGroup = await _subGroupsService.UpdateSubGroup(subGroup);

            if (await _subGroupsService.SaveChangesInContext())
            {
                return Ok(updatedSubGroup);
            }
            return BadRequest("Update Failed or the same subgroup was sent");
        }


        [HttpPost("delete")]
        public async Task<IActionResult> DeleteSubGroup(SubGroup subGroup)
        {
            _genericsRepo.Delete(subGroup);

            if (await _genericsRepo.SaveAll())
            {
                return NoContent();
            }
            return BadRequest("Delete Failed!");
        }
    }
}