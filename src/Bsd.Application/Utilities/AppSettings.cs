namespace Bsd.Application.Utilities
{
    public static class AppSettings
    {
        public static readonly string ApiUrl = GetEnvironmentVariable("SMARTCLOCK_API_URL");
        public static readonly string Identifier = GetEnvironmentVariable("SMARTCLOCK_IDENTIFIER");
        public static readonly string Token = GetEnvironmentVariable("SMARTCLOCK_TOKEN");
        public static readonly string DevelopmentEnvironment = GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        public static readonly string QaEnvironment = GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        private static string GetEnvironmentVariable(string variableName)
        {
            var value = Environment.GetEnvironmentVariable(variableName);
            if (value == null)
            {
                Console.WriteLine($"[WARNING] Variável de ambiente '{variableName}' não está configurada.");
            }
            return value ?? throw new Exception($"A variável de ambiente {variableName} não está configurada.");
        }
    }
}
