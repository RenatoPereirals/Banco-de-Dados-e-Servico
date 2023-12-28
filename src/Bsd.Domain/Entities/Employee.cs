using Bsd.Domain.Enums;

namespace Bsd.Domain.Entities
{
    public class Employee
    {
        // Campos privados
        private string _registration; 

        // Propriedades
        public string Registration
        {
            get => _registration;
            private set => SetRegistration(value);
        }
        public int Digit => CalculateDigit();
        public TypeService TypeService { get; set; }
        public Bsd Bsd { get; set; }

        // Construtor
        public Employee(string registration, TypeService typeService)
        {
            SetRegistration(registration);
            TypeService = typeService;
            Bsd = new Bsd();
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

        private void SetRegistration(string value)
        {
            if (value.Length != 4)
            {
                throw new ArgumentException("A matrícula deve conter 4 dígitos");
            }

            _registration = value;
        }
    }
}
