using System.ComponentModel.DataAnnotations;

namespace Bsd.Application.DTOs
{
    public class CreateBsdRequest
    {
        [Required(ErrorMessage = "O número do BSD é obrigatório.")]
        public int BsdNumber { get; set; }

        [Required(ErrorMessage = "A data do serviço é obrigatória.")]
        [DataType(DataType.Date)]
        public string DateService { get; set; } = string.Empty;

        [Required(ErrorMessage = "A matrícula do funcionário é obrigatória.")]
        public int EmployeeRegistration { get; set; }

        [Required(ErrorMessage = "O dígito é obrigatório.")]
        [Range(0, 9, ErrorMessage = "O dígito deve estar entre 0 e 9.")]
        public int Digit { get; set; }
    }
}