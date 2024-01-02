using Bsd.Domain.Enums;

namespace Bsd.Domain.Entities
{
    public class Employee
    {
        // Propriedades
        public string Registration { get; private set; } = string.Empty;
        public int Digit => CalculateDigit();
        public TypeService TypeService { get; set; } = new TypeService();
        public Bsd Bsd { get; set; } = new Bsd();

        // Construtor
        public Employee(string registration, TypeService typeService)
        {
            SetRegistration(registration);
            TypeService = typeService;
            Bsd = new Bsd();
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
