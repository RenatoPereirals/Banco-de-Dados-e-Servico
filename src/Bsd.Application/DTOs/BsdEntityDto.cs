using System.ComponentModel.DataAnnotations;
using Bsd.Domain.Entities;

namespace Bsd.Application.DTOs
{
    public class BsdEntityDto
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [RegularExpression(@"^\d{4}$", ErrorMessage = "O campo {0} deve conter 4 dígitos.")]
        public int Registration { get; set; }
        public int Digit { get; set; }
        public string RubricCode { get; set; } = string.Empty;
        public decimal TotalHours { get; set; }
    }
}