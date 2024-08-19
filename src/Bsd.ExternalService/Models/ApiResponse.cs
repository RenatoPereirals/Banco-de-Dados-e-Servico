namespace Bsd.ExternalService_.Models
{
    public class ApiResponse<T>
    {
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; } = string.Empty;
        public required T Obj { get; set; } 
    }
}