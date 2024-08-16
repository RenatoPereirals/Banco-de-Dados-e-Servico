using System.ComponentModel.DataAnnotations;

namespace Bsd.Application.DTOs
{
    public class ReportRequest
    {
        [Required(ErrorMessage = "O campo {0} é obrigatória.")]
        public string DataInicio { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo {0} é obrigatória.")]
        public string DataFim { get; set; } = string.Empty;
        public string OutputPath { get; set; } = string.Empty;
    }
}