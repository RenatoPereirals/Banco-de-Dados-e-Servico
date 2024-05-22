using System.ComponentModel.DataAnnotations;

namespace Bsd.Application.DTOs
{
    public class CreateBsdRequest
    {
        [Required]
        public int BsdNumber { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public string DateService { get; set; } = string.Empty;

        [Required]
        public int EmployeeRegistration { get; set; }

        [Required]
        [Range(0, 9)]
        public int Digit { get; set; }
    }
}