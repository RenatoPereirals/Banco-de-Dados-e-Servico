namespace Bsd.Application.DTOs
{
    public class MarkRequest
    {
        public MarkRequest(List<int> employeeIds)
        {
            MatriculaPessoa = employeeIds;
        }

        public List<int> MatriculaPessoa { get; set; }
        public string DataInicio { get; set; } = string.Empty;
        public string DataFim { get; set; } = string.Empty;
        public string ResponseType { get; set; } = "AS400V1";
    }
}
