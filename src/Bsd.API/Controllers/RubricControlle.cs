using Bsd.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Bsd.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RubricControlle : ControllerBase
    {
        private readonly IRubricApplicationService _rubricApplication;

        public RubricControlle(IRubricApplicationService rubricApplication)
        {
            _rubricApplication = rubricApplication;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateRubric(int rubricId, string description, decimal hoursPerDay, string dayType, string serviceType)
        {
            try
            {
                await _rubricApplication.CreateRubricAsync(rubricId, description, hoursPerDay, dayType, serviceType);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}