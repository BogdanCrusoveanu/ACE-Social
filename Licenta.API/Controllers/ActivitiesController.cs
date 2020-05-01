using Licenta.API.Data;
using Licenta.API.Services;
using Licenta.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Licenta.API.Controllers
{
    [ServiceFilter(typeof(LogUserActivity))]
    [Route("[controller]")]
    [ApiController]
    public class ActivitiesController : ControllerBase
    {
        private readonly ICoursesService _coursesService;
        private readonly ISeminarsService _seminarsService;
        private readonly ILaboratoriesService _laboratoriesService;
        private readonly ICompanyPresentationsService _companyPresentationsService;
        private readonly IGenericsRepository _genericsRepo;

        public ActivitiesController(ICoursesService coursesService,ISeminarsService seminarsService,
            ILaboratoriesService laboratoriesService, ICompanyPresentationsService companiesPresentationsService, IGenericsRepository genericsRepo)
        {
            _coursesService = coursesService;
            _seminarsService = seminarsService;
            _laboratoriesService = laboratoriesService;
            _companyPresentationsService = companiesPresentationsService;
            _genericsRepo = genericsRepo;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetActivitiesForUser(int id)
        {
            var courses = await _coursesService.GetCoursesForUser(id);
            var seminars =  await _seminarsService.GetSeminarsForUser(id);
            var laboratories =  await _laboratoriesService.GetLaboratoriesForUser(id);
            var companyPresentations =  await _companyPresentationsService.GetCompanyPresentationsForUser();

            var activitiesForReturn = _coursesService.GetActivitiesForUser(courses, laboratories, seminars, companyPresentations);

            return Ok(activitiesForReturn);
        }
    }
}