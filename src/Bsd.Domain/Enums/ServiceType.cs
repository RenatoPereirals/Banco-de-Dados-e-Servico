namespace Bsd.Domain.Enums
{
    [Flags]
    public enum ServiceType
    {
        None = 0,
        P110 = 1 << 0,
        P140 = 1 << 1,
        AllServices = P110 | P140
    }
}
