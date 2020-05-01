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
    public class SeminarsController : ControllerBase
    {
        private readonly ISeminarsService _seminarsService;
        private readonly IGenericsRepository _genericsRepo;

        public SeminarsController(ISeminarsService seminarsService, IGenericsRepository genericsRepo)
        {
            _seminarsService = seminarsService;
            _genericsRepo = genericsRepo;
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPost("add")]
        public async Task<IActionResult> AddSeminar(Seminar seminary)
        {
            int adminId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            if (await _seminarsService.SeminarExists(seminary, adminId))
            {
                return BadRequest("The seminary you entered already exists!");
            }

            _seminarsService.AddSeminar(seminary);

            if (await _genericsRepo.SaveAll())
            {
                return Ok("Seminary was added!");
            }

            return BadRequest("Something went wrong!");
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetSeminarsForAdmin(int id)
        {
            var seminars = await _seminarsService.GetSeminarsForUser(id);

            var seminarsForAdmin = _seminarsService.MapSeminarsForAdmin(seminars);

            return Ok(seminarsForAdmin);
        }


        [HttpPost("update")]
        public async Task<IActionResult> UpdateSeminar(Seminar seminar)
        {
            var updatedSeminar = await _seminarsService.UpdateSeminar(seminar);

            if (await _genericsRepo.SaveAll())
            {
                return Ok(updatedSeminar);
            }

            return BadRequest("You entered the same seminar name or something went wrong!");
        }
    }
}