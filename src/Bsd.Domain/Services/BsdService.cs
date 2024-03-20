using Bsd.Domain.Repository.Interfaces;
using Bsd.Domain.Services.Interfaces;

namespace Bsd.Domain.Services
{
    public class BsdService : IBsdService
    {
        private readonly IGeralRepository _geralRepository;
        private readonly IBsdRepository _bsdRepository;

        public BsdService(IGeralRepository geralRepository,
                          IBsdRepository bsdRepository)
        {
            _geralRepository = geralRepository;
            _bsdRepository = bsdRepository;
        }
    }
}