using AutoMapper;
using Bsd.API.Helpers;
using Bsd.Application.DTOs;
using Bsd.Application.Interfaces;
using Bsd.Domain.Repository.Interfaces;
using Bsd.Domain.Services.Interfaces;

namespace Bsd.Application.Services
{
    public class BsdApplicationService : IBsdApplicationService
    {
        private readonly IBsdRepository _bsdRepository;
        private readonly IMapper _mapper;
        private readonly IGeralRepository _geralRepository;
        private readonly IBsdService _bsdService;

        public BsdApplicationService(IMapper mapper,
                                     IBsdRepository bsdRepository,
                                     IGeralRepository geralRepository,
                                     IBsdService bsdService)
        {
            _mapper = mapper;
            _bsdRepository = bsdRepository;
            _geralRepository = geralRepository;
            _bsdService = bsdService;
        }

        public async Task CreateBsdAsync(int bsdNumber, string dateService, int employeeRegistration, int digit)
        {
            try
            {
                var parseDateService = DateHelper.ParseDate(dateService);
                await _bsdService.CreateBsdAsync(bsdNumber, parseDateService, employeeRegistration, digit);
                await _geralRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao tentar cria BSD. Erro: {ex.Message}");
            }
        }

        public async Task<IEnumerable<BsdEntityDto>> GetBsdEntitiesDtoByDateRangeAsync(string startDate, string endDate)
        {
            try
            {
                var parseStartDate = DateHelper.ParseDate(startDate);
                var parseEndDate = DateHelper.ParseDate(endDate);
                var bsdEntities = await _bsdRepository.GetEmployeeBsdEntitiesByDateRangeAsync(parseStartDate, parseEndDate);

                return _mapper.Map<IEnumerable<BsdEntityDto>>(bsdEntities);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao tentar resgatar bsd. Error: {ex.Message}");
            }
        }


    }
}