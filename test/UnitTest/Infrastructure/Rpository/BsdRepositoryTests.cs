using Bsd.Domain.Entities;
using Bsd.Domain.Repository.Interfaces;
using Bsd.Domain.Services.Interfaces;
using Bsd.Infrastructure.Context;
using Bsd.Infrastructure.RepositoryImpl;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Test.UnitTest.Infrastructure.Repository
{
    public class BsdRepositoryTests : IDisposable
    {
        private readonly BsdDbContext _context;
        private readonly BsdRepository _bsdRepository;
        private readonly Mock<IGeralRepository> _mockGeralRepository;
        private readonly Mock<IDayTypeChecker> _mockDayTypeChecker;

        public BsdRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<BsdDbContext>()
                .UseSqlite("DataSource=:memory:") // Conexão em memória
                .Options;

            _context = new BsdDbContext(options);
            _context.Database.OpenConnection(); // Abrir conexão
            _context.Database.EnsureCreated(); // Garantir que o banco de dados foi criado

            _mockGeralRepository = new Mock<IGeralRepository>();
            _mockDayTypeChecker = new Mock<IDayTypeChecker>();

            _bsdRepository = new BsdRepository(_context,
                                               _mockGeralRepository.Object);
        }

        [Fact]
        public async Task CreateBsdAsync_PersistsDataCorrectly()
        {
            // Arrange
            var bsd = CreateValidBsdEntity();

            // Act
            var result = await _bsdRepository.CreateBsdAsync(bsd);

            // Assert
            Assert.True(result, $"Sucesso ao criar Banco de dados:");
            var persistedEntity = await _context.BsdEntities.FirstOrDefaultAsync(e => e.BsdId == bsd.BsdId);
            Assert.NotNull(persistedEntity);
            Assert.Equal(bsd.BsdId, persistedEntity.BsdId);
            Assert.Equal(bsd.DateService, persistedEntity.DateService);
            Assert.Equal(bsd.DayType, persistedEntity.DayType);
        }

        private static BsdEntity CreateValidBsdEntity()
        {
            return new BsdEntity
            {
                BsdId = 123456,
                DateService = DateTime.Today,
                Employees = new List<Employee>(),
                EmployeeRubricAssignments = new List<EmployeeRubricAssignment>()
            };
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted(); // Excluir o banco de dados após os testes
            GC.SuppressFinalize(this);
            _context.Dispose();
        }
    }
}
