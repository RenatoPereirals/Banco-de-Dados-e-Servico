using Bsd.Domain.Enums;

namespace Bsd.Domain.Entities
{
    public class Employee
    {
        // Propriedades
        public string Registration { get; private set; } = string.Empty;
        public int Digit => CalculateDigit();
        public ServiceType ServiceType { get; } = new ServiceType();
        public int BsdId { get; }
        public DateTime DateService { get; set; }
        public IEnumerable<Bsd> Bsd { get; set; } = new List<Bsd>();
        public List<Rubric> Rubrics { get; set; } = new List<Rubric>();

        // Construtor
        public Employee(string registration, ServiceType serviceType, List<Rubric> rubrics)
        {
            SetRegistration(registration);
            ServiceType = serviceType;
            Rubrics = rubrics;
        }

        // Método para definir a matrícula
        private void SetRegistration(string value)
        {
            ValidateRegistration(value);
            Registration = value;
        }

        // Calcula o digito da matrícula com base no módulo 11
        private int CalculateDigit()
        {
            int sum = 0;
            int indice = 0;
            for (int i = Registration.Length; i > 0; i--)
            {
                sum += int.Parse(Registration[indice].ToString()) * (i + 1);
                indice++;
            }

            int mod = sum % 11;
            return 11 - mod;
        }

        private static void ValidateRegistration(string value)
        {
            if (value.Length != 4)
            {
                throw new ArgumentException("A matrícula deve conter 4 dígitos");
            }
        }
    }
}
