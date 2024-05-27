using System.Runtime.CompilerServices;
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

        public BsdApplicationService(IBsdRepository bsdRepository,
                                     IRubricService rubricService)
        {
            _bsdRepository = bsdRepository;
            _rubricService = rubricService;
        }

        public async Task<bool> CreateBsdAsync(CreateBsdRequest request)
        {
            try
            {
                var parseDateService = DateHelper.ParseDate(request.DateService);
                return await _bsdRepository.CreateBsdAsync(request.BsdNumber, parseDateService, request.EmployeeRegistration, request.Digit);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao tentar cria BSD. Erro: {ex.Message}");
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


    }
}