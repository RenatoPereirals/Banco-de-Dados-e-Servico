using Microsoft.AspNetCore.Mvc;

using Bsd.Application.Interfaces;
using Bsd.Application.Helpers.Interfaces;
using Bsd.Application.DTOs;

using Bsd.Domain.Repository.Interfaces;

namespace Bsd.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BsdController : ControllerBase
    {
        private readonly IBsdRepository _bsdRepository;
        private readonly IBsdApplicationService _bsdApplication;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEmployeeValidationService _employeeValidation;


        public BsdController(IBsdRepository bsdRepository,
                             IBsdApplicationService bsdApplication,
                             IEmployeeRepository employeeRepository,
                             IEmployeeValidationService employeeValidation)
        {
            _bsdRepository = bsdRepository;
            _bsdApplication = bsdApplication;
            _employeeRepository = employeeRepository;
            _employeeValidation = employeeValidation;

        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var bsdEntities = await _bsdRepository.GetAllBsdAsync();
            return Ok(bsdEntities);
        }

        [HttpGet("{bsdNumber}")]
        public async Task<IActionResult> GetBsdById(int bsdNumber)
        {
            var bsdEntity = await _bsdRepository.GetBsdByIdAsync(bsdNumber);
            return Ok(bsdEntity);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("create")]
        public async Task<IActionResult> Post([FromBody] CreateBsdRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var invalidRegistrations = await _employeeValidation.ValidateEmployeeRegistrationsAsync(request.EmployeeRegistrations);

            if (!invalidRegistrations)
                return NotFound($"Funcionários com as matrículas {string.Join(", ", request.EmployeeRegistrations)} não encontrados.");

            var createdBsd = await _bsdApplication.CreateBsdAsync(request);

            if (createdBsd == null)
                return NoContent();

            return CreatedAtAction(nameof(GetBsdById), new { id = createdBsd.BsdNumber }, createdBsd);
        }

        [HttpPost("addEmployee")]
        public async Task<IActionResult> AddEmployeeToBsdEntity(int bsdNumber)
        {
            var bsdEntity = await _bsdRepository.GetBsdByIdAsync(bsdNumber);
            return Ok();
        }
    }
}
