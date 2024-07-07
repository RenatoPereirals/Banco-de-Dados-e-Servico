using Bsd.Application.Interfaces;
using Bsd.Application.DTOs;

using Microsoft.AspNetCore.Mvc;

namespace Bsd.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BsdController : ControllerBase
    {
        private readonly IBsdApplicationService _bsdApplication;


        public BsdController(IBsdApplicationService bsdApplication)
        {
            _bsdApplication = bsdApplication;

        }

        [HttpGet]
        public Task<IActionResult> GetAll()
        {
            throw new NotImplementedException();
        }

        [HttpGet("{bsdNumber}")]
        public Task<IActionResult> GetBsdById(int bsdNumber)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("bsd")]
        public async Task<IActionResult> Post([FromBody] CreateBsdRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdBsd = await _bsdApplication.CreateBsdAsync(request);

            if (createdBsd == null)
                return NoContent();

            return CreatedAtAction(nameof(GetBsdById), new { id = createdBsd.BsdNumber }, createdBsd);
        }

        [HttpPost("addEmployee")]
        public Task<IActionResult> AddEmployeeToBsdEntity(int bsdNumber)
        {
            throw new NotImplementedException();
        }
    }
}
