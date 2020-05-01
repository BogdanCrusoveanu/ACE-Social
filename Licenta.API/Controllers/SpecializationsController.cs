using Licenta.API.Models;
using Licenta.API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Licenta.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SpecializationsController : ControllerBase
    {
        private readonly ISpecializationsService _specializationsService;

        public SpecializationsController(ISpecializationsService specializationsService)
        {
            _specializationsService = specializationsService;
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetSpecializations()
        {
            var specializations = await _specializationsService.GetSpecializations();

            var specialiationsToReturn = _specializationsService.MapSpecializations(specializations);

            return Ok(specialiationsToReturn);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddSpecizalization(Specialization specialization)
        {
            if (await _specializationsService.SpecializationExists(specialization))
            {
                return BadRequest("The specialization you entered already exists!");
            }

            _specializationsService.AddSpecialization(specialization);

            if (await _specializationsService.SaveChangesInContext())
            {
                return NoContent();
            }

            return BadRequest("Something went wrong!");
        }


        [HttpPost("update")]
        public async Task<IActionResult> UpdateSpecialization(Specialization specialization)
        {
            var updatedSpecialization = await _specializationsService.UpdateSpecialization(specialization);

            if (await _specializationsService.SaveChangesInContext())
            {
                return Ok(updatedSpecialization);
            }
            return BadRequest("Update Failed or the same specialization was sent");
        }
    }
}