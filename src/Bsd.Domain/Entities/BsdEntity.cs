﻿using System.ComponentModel.DataAnnotations;
using Bsd.Domain.Enums;
namespace Bsd.Domain.Entities
{
    public class BsdEntity
    {
        public BsdEntity()
        {
            Employees = new HashSet<Employee>();
            EmployeeRubrics = new HashSet<EmployeeRubric>();
        }

        [Key]
        public int BsdId { get; set; }
        public DateTime DateService { get; set; }
        public DayType DayType { get; set; }
        public ICollection<Employee> Employees { get; set; }
        public ICollection<EmployeeRubric> EmployeeRubrics { get; set; }
    }
}
