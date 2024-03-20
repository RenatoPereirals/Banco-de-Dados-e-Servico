using Bsd.Application.Interfaces;
using Bsd.Domain.Repository.Interfaces;
using Bsd.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS.Core;

namespace Bsd.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BsdController : ControllerBase
    {
        private readonly IBsdRepository _bsdRepository;
        private readonly IBsdApplicationService _bsdApplication;
        private readonly IEmployeeRepository _employeeRepository;


        public BsdController(IBsdRepository bsdRepository,
                             IBsdApplicationService bsdApplication,
                             IEmployeeRepository employeeRepository)
        {
            _bsdRepository = bsdRepository;
            _bsdApplication = bsdApplication;
            _employeeRepository = employeeRepository;

        }

        [HttpGet("{startDate}/{endDate}")]
        public async Task<IActionResult> GetBsdEntities(string startDate, string endDate)
        {
            try
            {
                var bsd = await _bsdApplication.GetBsdEntitiesDtoByDateRangeAsync(startDate, endDate);
                if (bsd == Empty || bsd == null)
                    return BadRequest($"Nenhum BSD pode ser encontrado entre as datas {startDate} e {endDate}.");


                return Ok(bsd);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBsdEntiteis()
        {
            try
            {
                var bsdEntities = await _bsdRepository.GetAllBsdAsync();
                return Ok(bsdEntities);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{bsdNumber}")]
        public async Task<IActionResult> GetBsdEntity(int bsdNumber)
        {
            try
            {
                var bsdEntity = await _bsdRepository.GetBsdByIdAsync(bsdNumber);
                return Ok(bsdEntity);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreatedBsdEntity(int bsdNumber, string dateService, int employeeRegistration, int digit)
        {
            try
            {
                var employee = await _employeeRepository.GetEmployeeByRegistrationAsync(employeeRegistration)
                    ?? throw new Exception($"Funcionário com a matrícula {employeeRegistration} não encontrado.");

                await _bsdApplication.CreateBsdAsync(bsdNumber, dateService, employeeRegistration, digit);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("addEmployee")]
        public async Task<IActionResult> AddEmployeeToBsdEntity(int bsdNumber)
        {
            try
            {
                var bsdEntity = await _bsdRepository.GetBsdByIdAsync(bsdNumber);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
