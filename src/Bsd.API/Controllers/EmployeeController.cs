using Bsd.Application.Interfaces;
using Bsd.Application.Helpers.Interfaces;
using Bsd.Application.DTOs;

using Bsd.Domain.Repository.Interfaces;
using Bsd.Domain.Services.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace Bsd.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEmployeeService _employeeService;
        private readonly IGeralRepository _geralRepository;
        private readonly IEmployeeApplicationService _employeeApplication;
        private readonly IEmployeeValidationService _employeeValidation;
        public EmployeeController(IEmployeeRepository employeeRepository,
                                  IGeralRepository geralRepository,
                                  IEmployeeService employeeService,
                                  IEmployeeApplicationService employeeApplication,
                                  IEmployeeValidationService employeeValidation)
        {
            _employeeRepository = employeeRepository;
            _employeeService = employeeService;
            _geralRepository = geralRepository;
            _employeeApplication = employeeApplication;
            _employeeValidation = employeeValidation;

        }

        [HttpGet("{registration}")]
        public async Task<IActionResult> GetEmployeeById(int registration)
        {
            try
            {
                var employee = await _employeeRepository.GetEmployeeByRegistrationAsync(registration);

                return Ok(employee);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet()]
        public async Task<IActionResult> GetEmployees()
        {
            try
            {
                var employees = await _employeeRepository.GetAllEmployees();

                return Ok(employees);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("created")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] EmployeeRequestDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var invalidRegistration = await _employeeValidation.ValidateEmployeeRegistrationAsync(request.EmployeeId);

            if (!invalidRegistration)
                return BadRequest($"Matrícula {string.Join(", ", request.EmployeeId)} não é valida.");

            var employee = await _employeeApplication.CreateEmployeeAsync(request);

            if (employee == null)
                return NoContent();

            return CreatedAtAction(nameof(GetEmployeeById), new { id = employee.EmployeeId }, employee);
        }
    }
}