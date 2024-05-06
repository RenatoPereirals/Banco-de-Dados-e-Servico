using Bsd.Domain.Enums;
namespace Bsd.Domain.Entities
{
    public class BsdEntity
    {
        private int _bsdNumber;
        private DateTime _dateService;
        private DayType _dayTape;
        private ICollection<EmployeeBsdEntity> _employeeBsdEntities = new List<EmployeeBsdEntity>();

        public int BsdNumber
        {
            get { return _bsdNumber; }
            set { _bsdNumber = value; }
        }

        public DateTime DateService
        {
            get { return _dateService; }
            set { _dateService = value; }
        }

        public DayType DayType
        {
            get { return _dayTape; }
            set { _dayTape = value; }
        }

        public ICollection<EmployeeBsdEntity> EmployeeBsdEntities
        {
            get { return _employeeBsdEntities; }
            set { _employeeBsdEntities = value; }
        }

    }
}
