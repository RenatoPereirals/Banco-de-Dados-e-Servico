using System.ComponentModel.DataAnnotations;

namespace Bsd.Application.DTOs
{
    public class EmployeeBsdEntityDto
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [RegularExpression(@"^\d{4}$", ErrorMessage = "O campo {0} deve conter 4 dígitos.")]
        public int Registration { get; set; }
        public int Digit { get; set; }
    }
}