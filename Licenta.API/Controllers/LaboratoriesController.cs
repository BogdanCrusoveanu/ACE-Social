using Licenta.API.Data;
using Licenta.API.Models;
using Licenta.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Licenta.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LaboratoriesController : ControllerBase
    {
        private readonly ILaboratoriesService _laboratoriesService;
        private readonly IGenericsRepository _genericsRepo;

        public LaboratoriesController(ILaboratoriesService laboratoriesService, IGenericsRepository genericsRepo)
        {
            _laboratoriesService = laboratoriesService;
            _genericsRepo = genericsRepo;
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetLaboratoriesForAdmin(int id)
        {
            var laboratories = await _laboratoriesService.GetLaboratoriesForUser(id);

            var laboratoriesForAdmin = _laboratoriesService.MapLaboratoriesForAdmin(laboratories);

            return Ok(laboratoriesForAdmin);
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPost("add")]
        public async Task<IActionResult> AddLaboratory(Laboratory laboratory)
        {
            int adminId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            if (await _laboratoriesService.LaboratoryExists(laboratory, adminId))
            {
                return BadRequest("The laboratory you entered already exists!");
            }

            _laboratoriesService.AddLaboratory(laboratory);

            if (await _genericsRepo.SaveAll())
            {
                return Ok("Laboratory was added!");
            }

            return BadRequest("Something went wrong!");
        }


        [HttpPost("update")]
        public async Task<IActionResult> UpdateLaboratory(Laboratory laboratory)
        {
            var updatedLaboratory = await _laboratoriesService.UpdateLaboratory(laboratory);

            if (await _genericsRepo.SaveAll())
            {
                return Ok(updatedLaboratory);
            }

            return BadRequest("You entered the same class name or something went wrong!");
        }
    }
}