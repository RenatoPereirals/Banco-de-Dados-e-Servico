using System.Collections.ObjectModel;
using System.Data.Common;
using Bsd.Domain.Enums;

namespace Bsd.Domain.Entities
{
    public class Bsd
    {
        public string BsdNumber { get; }
        public IEnumerable<Employee> Employee { get; }
        public DateTime DateService { get; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public TypeDay TypeDay { get; }

        public Bsd(string bsdNumber, IEnumerable<Employee> employees, DateTime dateService)
        {
            ValidateInput(bsdNumber, employees, dateService);
            BsdNumber = bsdNumber;
            Employee = employees;
            DateService = dateService;
        }

        private static void ValidateInput(string bsdNumber, IEnumerable<Employee> employees, DateTime dateService)
        {
            if (string.IsNullOrEmpty(bsdNumber))
            {
                throw new ArgumentException("Número BSD inválido", nameof(bsdNumber));
            }
            if(employees == null)
            {
                throw new ArgumentException("Deve conter um número de matrícula do funcionário", nameof(employees));
            }
        }

    }
}
