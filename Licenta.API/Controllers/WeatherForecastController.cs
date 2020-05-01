using Licenta.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Licenta.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly DataContext _context;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, DataContext context)
        {
            _logger = logger;
            _context = context;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var weather = await _context.Weather.ToListAsync();

            return Ok(weather);
        }
        [Authorize(Roles = "Member")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetValue(int id)
        {
            var weather = await _context.Weather.FirstOrDefaultAsync(x => x.Id == id);

            return Ok(weather);
        }
    }
}
