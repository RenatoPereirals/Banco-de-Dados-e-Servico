using AutoMapper;
using Bsd.API.Helpers;
using Bsd.Application.DTOs;
using Bsd.Application.Interfaces;
using Bsd.Domain.Entities;
using Bsd.Domain.Repository.Interfaces;

namespace Bsd.Application.Services
{
    public class BsdApplicationService : IBsdApplicationService
    {
        private readonly IBsdRepository _bsdRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public BsdApplicationService(IBsdRepository bsdRepository,
                                     IEmployeeRepository employeeRepository,
                                     IMapper mapper)
        {
            _bsdRepository = bsdRepository;
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<CreateBsdRequest> CreateBsdAsync(CreateBsdRequest request)
        {
            RequestValidation(request);

            try
            {
                request.DateServiceDate = DateHelper.ParseDate(request.DateService);

                var bsd = _mapper.Map<BsdEntity>(request);

                if (await _bsdRepository.CreateBsdAsync(bsd))
                {
                    foreach (var registration in request.EmployeeRegistrations)
                    {
                        var employee = await _employeeRepository.GetEmployeeByRegistrationAsync(registration);
                        if (employee != null)
                        {
                            await _bsdRepository.AddEmployeeToBsdAsync(bsd.BsdId, employee.EmployeeId);
                        }
                    }
                    var bsdReturn = await _bsdRepository.GetBsdByIdAsync(bsd.BsdId);
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
            if (request.Digit < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(request), "Digit não pode ser negativo");
            }
        }

    }
}