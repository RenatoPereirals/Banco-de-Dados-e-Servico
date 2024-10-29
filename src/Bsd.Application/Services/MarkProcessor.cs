using Bsd.Application.DTOs;
using Bsd.Application.Interfaces;

using Bsd.Domain.Entities;
using Bsd.Domain.Services.Interfaces;

namespace Bsd.Application.Services
{
    public class MarkProcessor : IMarkProcessor
    {
        private readonly IStaticDataService _staticDataService;

        public MarkProcessor(IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
        }

        public ICollection<Employee> ProcessMarks(ICollection<MarkResponse> markResponses)
        {

            List<List<MarkResponse>> groupedMarks = GroupMarksByEmployee(markResponses);

            var employees = new List<Employee>();

            foreach (var employeeMarks in groupedMarks)
            {
                Employee employee = AssignWorkedDaysToEmployee(employeeMarks);
                if (employee != null)
                    employees.Add(employee);
            }

            return employees;
        }

        private List<List<MarkResponse>> GroupMarksByEmployee(ICollection<MarkResponse> marks)
        {
            var grouped = marks
                .GroupBy(m => m.Matricula)
                .Select(g => g.OrderBy(m => new DateTime(m.Ano, m.Mes, m.Dia, m.Hora, m.Minuto, 0)).ToList())
                .ToList();

            return grouped;
        }

        private Employee AssignWorkedDaysToEmployee(List<MarkResponse> employeeMarks)
        {
            var employees = _staticDataService.GetEmployees();
            var employee = employees.FirstOrDefault(e => e.EmployeeId == employeeMarks.First().Matricula) 
                ?? throw new Exception("Employee not found");

            var workedDays = GetWorkedDays(employeeMarks);

            foreach (var workedDay in workedDays)
            {
                // Verifica se já existe um dia com a mesma Data de Entrada e Hora de Início
                bool isDuplicate = employee.WorkedDays.Any(wd =>
                    wd.DateEntry == workedDay.DateEntry && wd.StartTime == workedDay.StartTime);

                if (!isDuplicate)
                {
                    employee.WorkedDays.Add(workedDay);
                }
            }

            return employee;
        }

        private List<WorkedDay> GetWorkedDays(List<MarkResponse> employeeMarks)
        {
            var workedDays = new List<WorkedDay>();

            MarkResponse? startMark = null;

            for (int i = 0; i < employeeMarks.Count; i++)
            {
                var currentMark = employeeMarks[i];
                var currentDate = new DateTime(currentMark.Ano, currentMark.Mes, currentMark.Dia);

                // Se startMark for null, significa que estamos buscando uma marcação de entrada
                if (startMark == null)
                {
                    startMark = currentMark;
                }
                else
                {
                    // Se startMark já está preenchido, o currentMark é tratado como marcação de saída
                    var endMark = currentMark;

                    // Garante que a marcação de saída é do mesmo dia
                    if (startMark.Ano == endMark.Ano && startMark.Mes == endMark.Mes && startMark.Dia == endMark.Dia)
                    {
                        // Adiciona o dia trabalhado
                        workedDays.Add(CreateWorkedDay(startMark, endMark));

                        // Reseta startMark para buscar a próxima entrada
                        startMark = null;
                    }
                    else
                    {
                        // Se as marcações não forem do mesmo dia, reinicia a marcação de entrada para o próximo dia
                        startMark = currentMark;
                    }
                }
            }

            return workedDays;
        }

        private WorkedDay CreateWorkedDay(MarkResponse startMark, MarkResponse endMark)
        {
            var dateEntry = new DateTime(startMark.Ano, startMark.Mes, startMark.Dia);

            var dateExit = (startMark.Dia == endMark.Dia)
                ? new DateTime(endMark.Ano, endMark.Mes, endMark.Dia).AddDays(1)
                : new DateTime(endMark.Ano, endMark.Mes, endMark.Dia);

            var workedDay = new WorkedDay
            {
                DateEntry = dateEntry,
                DateExit = dateExit,
                StartTime = new TimeOnly(startMark.Hora, startMark.Minuto),
                EndTime = new TimeOnly(endMark.Hora, endMark.Minuto)
            };

            return workedDay;
        }

    }
}
