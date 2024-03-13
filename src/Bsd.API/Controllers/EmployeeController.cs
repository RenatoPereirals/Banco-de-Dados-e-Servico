using Bsd.Application.Interfaces;
using Bsd.Domain.Entities;
using Bsd.Domain.Enums;
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
        public EmployeeController(IEmployeeRepository employeeRepository,
                                  IGeralRepository geralRepository,
                                  IEmployeeService employeeService,
                                  IEmployeeApplicationService employeeApplication)
        {
            _employeeRepository = employeeRepository;
            _employeeService = employeeService;
            _geralRepository = geralRepository;
            _employeeApplication = employeeApplication;

        }

        [HttpGet("{registration}")]
        public async Task<IActionResult> GetEmployeeByRegistration(int registration)
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

        [HttpPost("created/{registration}")]
        public async Task<IActionResult> CreateEmployee(int registration, string serviceType)
        {
            try
            {
                await _employeeApplication.CreateEmployeeAsync(registration, serviceType);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}