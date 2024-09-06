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

        public ICollection<Employee> ProcessMarks(ICollection<MarkResponse> marks)
        {
            var groupedMarks = GroupMarksByEmployee(marks);
            var employees = new List<Employee>();

            foreach (var employeeMarks in groupedMarks)
            {
                var employee = AssignWorkedDaysToEmployee(employeeMarks);
                if (employee != null)
                {
                    employees.Add(employee);
                }
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
            var employee = employees.FirstOrDefault(e => e.EmployeeId == employeeMarks.First().Matricula) ?? throw new Exception("Employee not found");
            var workedDays = GetWorkedDays(employeeMarks);

            foreach (var workedDay in workedDays)
            {
                employee.WorkedDays.Add(workedDay);
            }

            return employee;
        }

        private List<WorkedDay> GetWorkedDays(List<MarkResponse> employeeMarks)
        {
            var workedDays = new List<WorkedDay>();

            List<MarkResponse> sortedMarks = employeeMarks.OrderBy(m => new DateTime(m.Ano, m.Mes, m.Dia, m.Hora, m.Minuto, 0)).ToList();
            MarkResponse startMark = sortedMarks.First();
            MarkResponse endMark = startMark;

            bool hasContinuousMarks = false;

            Console.WriteLine($"Processando {sortedMarks.Count} marca��es para o funcion�rio {startMark.Matricula}...");

            for (int i = 1; i < sortedMarks.Count; i++)
            {
                var currentMark = sortedMarks[i];
                var currentDate = new DateTime(currentMark.Ano, currentMark.Mes, currentMark.Dia);
                var endDate = new DateTime(endMark.Ano, endMark.Mes, endMark.Dia);

                if (currentDate.Date == endDate.Date.AddDays(1))
                {
                    endMark = currentMark;
                    workedDays.Add(CreateWorkedDay(startMark, endMark));
                    hasContinuousMarks = true;
                }
                else
                {
                    startMark = currentMark;
                    endMark = currentMark;
                }
            }

            if (!hasContinuousMarks)
                workedDays.Clear();

            return workedDays;
        }

        private WorkedDay CreateWorkedDay(MarkResponse startMark, MarkResponse endMark)
        {
            var workedDay = new WorkedDay
            {
                DateEntry = new DateTime(startMark.Ano, startMark.Mes, startMark.Dia),
                DateExit = new DateTime(endMark.Ano, endMark.Mes, endMark.Dia),
                StartTime = new TimeOnly(startMark.Hora, startMark.Minuto),
                EndTime = new TimeOnly(endMark.Hora, endMark.Minuto)
            };

            return workedDay;
        }
    }
}
