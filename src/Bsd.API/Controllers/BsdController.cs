using Bsd.Domain.Entities;
using Bsd.Domain.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Bsd.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BsdController : ControllerBase
    {
        private readonly IBsdRepository _bsdRepository;

        public BsdController(IBsdRepository bsdRepository)
        {
            _bsdRepository = bsdRepository;
        }

        [HttpGet("{startDate}-{endDate}")]
        public async Task<ActionResult<IEnumerable<EmployeeBsdEntity>>> GetEmployeeBsdEntities(string startDate, string endDate)
        {
            var parseStartDate = DateTime.Parse(startDate);
            var parseEndDate = DateTime.Parse(endDate);
            var employeeBsdEntities = await _bsdRepository.GetEmployeeBsdEntitiesByDateRangeAsync(parseStartDate, parseEndDate);
            return Ok(employeeBsdEntities);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeBsdEntity>>> GetAllBsdEntiteis()
        {
            var bsdEntities = await _bsdRepository.GetAllBsdAsync();
            return Ok(bsdEntities);
        }

        [HttpGet("{bsdNumber}")]
        public async Task<ActionResult<IEnumerable<EmployeeBsdEntity>>> GetBsdEntity(int bsdNumber)
        {
            var bsdEntity = await _bsdRepository.GetBsdByIdAsync(bsdNumber);
            return Ok(bsdEntity);
        }
    }
}
