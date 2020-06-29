using Licenta.API.Data;
using Licenta.API.Models;
using Licenta.API.Services;
using Licenta.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Licenta.API.Controllers
{
    [ServiceFilter(typeof(LogUserActivity))]
    [Route("[controller]")]
    [ApiController]
    public class CompanyPresentationsController : ControllerBase
    {
        private readonly ICompanyPresentationsService _companyPresentationsService;
        private readonly IGenericsRepository _genericsRepo;

        public CompanyPresentationsController(ICompanyPresentationsService companyPresentationsService, IGenericsRepository genericsRepo)
        {
            _companyPresentationsService = companyPresentationsService;
            _genericsRepo = genericsRepo;
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPost("add")]
        public async Task<IActionResult> AddPresentation(CompanyPresentation presentation)
        {
            if (await _companyPresentationsService.CompanyPresentationExists(presentation))
            {
                return BadRequest("There is a presentation already at this hour or in this class!");
            }

            _companyPresentationsService.AddCompanyPresentation(presentation);

            if (await _genericsRepo.SaveAll())
            {
                return NoContent();
            }

            return BadRequest("Something went wrong!");
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetCompanyPresentationsForAdmin(int id)
        {
            var presentations = await _companyPresentationsService.GetCompanyPresentationsForUser();

            var presentationsForAdmin = _companyPresentationsService.MapPresentationsForAdmin(presentations);

            return Ok(presentationsForAdmin);
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdatePresentation(CompanyPresentation presentation)
        {
            var updatedPresentation = await _companyPresentationsService.UpdatePresentation(presentation);

            if (await _genericsRepo.SaveAll())
            {
                return Ok(updatedPresentation);
            }
            return BadRequest("Update Failed or the same presentation was sent");
        }

        [HttpPost("delete")]
        public async Task<IActionResult> DeletePresentation(CompanyPresentation presentation)
        {
            _genericsRepo.Delete(presentation);

            if (await _genericsRepo.SaveAll())
            {
                return NoContent();
            }
            return BadRequest("Delete Failed!");
        }
    }
}