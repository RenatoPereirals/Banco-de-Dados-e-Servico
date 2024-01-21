﻿using Bsd.Domain.Enums;
namespace Bsd.Domain.Entities
{
    public class BsdEntity
    {
        public BsdEntity(int bsdNumber,
                         DateTime dateService)
        {
            BsdNumber = bsdNumber;
            DateService = dateService;
        }

        public int BsdNumber { get; set; }
        public DateTime DateService { get; }
        public DayType DayType { get; set; }
        public ICollection<EmployeeBsdEntity> EmployeeBsdEntities { get; set; } = new List<EmployeeBsdEntity>();

    }
}
