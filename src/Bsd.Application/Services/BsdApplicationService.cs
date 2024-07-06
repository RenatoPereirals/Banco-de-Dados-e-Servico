using Bsd.Application.Interfaces;
using Bsd.Application.DTOs;

using Bsd.Domain.Services.Interfaces;
using Bsd.Domain.Repository.Interfaces;
using Bsd.Domain.Entities;

using AutoMapper;

namespace Bsd.Application.Services
{
    public class BsdApplicationService : IBsdApplicationService
    {
        private readonly IBsdRepository _bsdRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDateHelper _dateHelper;
        private readonly IMapper _mapper;

        public BsdApplicationService(IBsdRepository bsdRepository,
                                     IEmployeeRepository employeeRepository,
                                     IDateHelper dateHelper,
                                     IMapper mapper)
        {
            _bsdRepository = bsdRepository;
            _employeeRepository = employeeRepository;
            _dateHelper = dateHelper;
            _mapper = mapper;
        }

        public async Task<CreateBsdRequest> CreateBsdAsync(CreateBsdRequest request)
        {
            RequestValidation(request);

            request.DateServiceDate = _dateHelper.ParseDate(request.DateService);

            var bsd = _mapper.Map<BsdEntity>(request);

            if (await _bsdRepository.CreateBsdAsync(bsd))
            {
                foreach (var registration in request.EmployeeRegistrations)
                {
                    var employee = await _employeeRepository.GetEmployeeByRegistrationAsync(registration);
                    if (employee != null)
                    {
                        await _bsdRepository.AddEmployeeToBsdAsync(bsd);
                    }
                }
                var bsdReturn = await _bsdRepository.GetBsdByIdAsync(bsd.BsdId);
                return _mapper.Map<CreateBsdRequest>(bsdReturn);
            }

            throw new ApplicationException("Falha ao criar BSD no banco de dados.");
        }

        private static void RequestValidation(CreateBsdRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request), "Você precisa preencher todos os dados.");
            }

            var bsdNumer = request.BsdNumber.ToString();

            if (bsdNumer.Length != 6)
            {
                throw new ArgumentOutOfRangeException(nameof(request), "O número do BSD deve conter 6 digitos.");
            }

            if (request.BsdNumber <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(request), "O número do BSD não pode ser negativo.");
            }

            if (string.IsNullOrWhiteSpace(request.DateService))
            {
                throw new ArgumentException("DateService não pode ser vazio ou nulo.", nameof(request));
            }

            if (request.Digit < 0 || request.Digit > 11)
            {
                throw new ArgumentOutOfRangeException(nameof(request), "Digito incorreto.");
            }
        }

    }
}