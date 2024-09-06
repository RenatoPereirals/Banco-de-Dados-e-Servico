using Bsd.Domain.Entities;

namespace Bsd.Domain.Services.Interfaces;

public interface IBsdService
{
    Task CreateOrUpdateBsdsAsync(BsdEntity bsd);
}