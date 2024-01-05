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
        public IEnumerable<Bsd> Bsd { get; set; } = new List<Bsd>();

        // Construtor
        public Employee(string registration, ServiceType serviceType)
        {
            SetRegistration(registration);
            ServiceType = serviceType;
        }

        // Método para definir a matrícula
        public void SetRegistration(string value)
        {
            ValidateRegistration(value);
            Registration = value;
        }

        // Métodos
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
