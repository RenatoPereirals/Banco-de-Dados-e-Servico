using System.ComponentModel.DataAnnotations;

namespace Bsd.Application.DTOs
{
    public class CreateBsdRequest
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [RegularExpression(@"^\d{6}$", ErrorMessage = "O campo {0} deve conter 6 dígitos.")]
        public int BsdNumber { get; set; }

        [Required(ErrorMessage = "A data do serviço é obrigatória.")]
        [DataType(DataType.Date)]
        public string DateService { get; set; } = string.Empty;

        public DateTime DateServiceDate { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatória.")]
        [RegularExpression(@"^\d{5}$", ErrorMessage = "O campo {0} deve conter 5 dígitos.")]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [Range(0, 9, ErrorMessage = "O campo {0} deve estar entre 0 e 9.")]
        [RegularExpression(@"^\d{1}$", ErrorMessage = "O campo {0} deve conter 1 dígitos.")]
        public int Digit { get; set; }
    }
}