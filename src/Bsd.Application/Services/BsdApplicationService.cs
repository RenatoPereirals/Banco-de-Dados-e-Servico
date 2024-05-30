using AutoMapper;
using Bsd.API.Helpers;
using Bsd.Application.DTOs;
using Bsd.Application.Interfaces;
using Bsd.Domain.Entities;
using Bsd.Domain.Repository.Interfaces;
using Bsd.Domain.Services.Interfaces;

namespace Bsd.Application.Services
{
    public class BsdApplicationService : IBsdApplicationService
    {
        private readonly IBsdRepository _bsdRepository;
        private readonly IRubricService _rubricService;
        private readonly IGeralRepository _geralRepository;
        private readonly IMapper _mapper;

        public BsdApplicationService(IBsdRepository bsdRepository,
                                     IRubricService rubricService,
                                     IGeralRepository geralRepository,
                                     IMapper mapper)
        {
            _bsdRepository = bsdRepository;
            _rubricService = rubricService;
            _geralRepository = geralRepository;
            _mapper = mapper;
        }

        public async Task<CreateBsdRequest> CreateBsdAsync(CreateBsdRequest request)
        {
            RequestValidation(request);

            try
            {
                var parseDateService = DateHelper.ParseDate(request.DateService);
                var bsd = _mapper.Map<BsdEntity>(request);

                _geralRepository.Create<BsdEntity>(bsd);

                if (await _geralRepository.SaveChangesAsync())
                {
                    var bsdReturn = await _bsdRepository.GetBsdByIdAsync(bsd.BsdNumber);
                    return _mapper.Map<CreateBsdRequest>(bsdReturn);
                }

                throw new ApplicationException("Falha ao salvar as alterações no banco de dados.");
            }
            catch (Exception)
            {
                //_logger.LogError(ex, "Erro ao tentar criar BSD.");
                throw new ApplicationException("Erro ao tentar criar BSD. Consulte o log para mais detalhes.");
            }
        }

        public async Task<IEnumerable<EmployeeRubricHours>> GetBsdEntitiesDtoByDateRangeAsync(string startDate, string endDate)
        {
            try
            {
                var parseStartDate = DateHelper.ParseDate(startDate);
                var parseEndDate = DateHelper.ParseDate(endDate);
                var bsdEntities = await _rubricService.GetEmployeeRubricHoursAsync(parseStartDate, parseEndDate);

                return bsdEntities;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao tentar resgatar bsd. Error: {ex.Message}");
            }
        }

        private static void RequestValidation(CreateBsdRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request), "Você precisa preencher todos os dados.");
            }
            if (request.BsdNumber <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(request));
            }
            if (string.IsNullOrWhiteSpace(request.DateService))
            {
                throw new ArgumentException("DateService não pode ser vazio ou nulo", nameof(request));
            }
            if (request.EmployeeRegistration <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(request), "EmployeeRegistration deve ser maior que zero");
            }
            if (request.Digit < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(request), "Digit não pode ser negativo");
            }
        }

    }
}