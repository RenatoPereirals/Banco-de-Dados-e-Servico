using Bsd.Domain.Entities;
using Bsd.Domain.Enums;
using Bsd.Domain.Repository.Interfaces;
using Bsd.Domain.Services.Interfaces;
using Bsd.Infrastructure.Context;
using Bsd.Infrastructure.RepositoryImpl;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;
using System.Linq;

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

            _bsdRepository = new BsdRepository(_context, _mockGeralRepository.Object, _mockDayTypeChecker.Object);
        }

        [Fact]
        public async Task CreateBsdAsync_ReturnsTrue_WhenValidInput()
        {
            // Arrange
            var bsd = CreateValidBsdEntity();
            SetupDayTypeChecker(DayType.HoliDay);
            SetupGeralRepositorySaveChangesAsync(true);

            // Act
            var result = await _bsdRepository.CreateBsdAsync(bsd);

            // Assert
            Assert.True(result);
            _mockGeralRepository.Verify(r => r.Create(It.IsAny<BsdEntity>()), Times.Once);
            _mockGeralRepository.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task CreateBsdAsync_ThrowsDbUpdateException_WhenDbUpdateException()
        {
            // Arrange
            var bsd = CreateValidBsdEntity();
            SetupDayTypeChecker(DayType.Sunday);
            SetupGeralRepositorySaveChangesAsyncException(new DbUpdateException());

            // Act & Assert
            await Assert.ThrowsAsync<DbUpdateException>(() => _bsdRepository.CreateBsdAsync(bsd));
        }

        [Fact]
        public async Task CreateBsdAsync_ThrowsException_WhenGenericException()
        {
            // Arrange
            var bsd = CreateValidBsdEntity();
            _mockDayTypeChecker.Setup(d => d.GetDayType(It.IsAny<DateTime>())).Throws(new Exception());

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _bsdRepository.CreateBsdAsync(bsd));
        }

        [Fact]
        public async Task CreateBsdAsync_PersistsDataCorrectly()
        {
            // Arrange
            var bsd = CreateValidBsdEntity();
            SetupDayTypeChecker(DayType.HoliDay);

            // Configurar o mock para adicionar a entidade ao contexto real
            _mockGeralRepository.Setup(r => r.Create(It.IsAny<BsdEntity>())).Callback<BsdEntity>(bsdEntity =>
            {
                _context.BsdEntities.Add(bsdEntity);
                _context.SaveChanges();
            });
            SetupGeralRepositorySaveChangesAsync(true);

            // Act
            var result = await _bsdRepository.CreateBsdAsync(bsd);

            // Assert
            Assert.True(result);
            var persistedEntity = _context.BsdEntities.FirstOrDefault(e => e.BsdId == bsd.BsdId);
            Assert.NotNull(persistedEntity);
            Assert.Equal(bsd.BsdId, persistedEntity.BsdId);
            Assert.Equal(bsd.DateService, persistedEntity.DateService);
            Assert.Equal(DayType.HoliDay, persistedEntity.DayType);
        }

        private static BsdEntity CreateValidBsdEntity()
        {
            return new BsdEntity
            {
                BsdId = 1234,
                DateService = DateTime.Today,
                Employees = new List<Employee>()
            };
        }

        private void SetupDayTypeChecker(DayType dayType)
        {
            _mockDayTypeChecker.Setup(d => d.GetDayType(It.IsAny<DateTime>())).Returns(dayType);
        }

        private void SetupGeralRepositorySaveChangesAsync(bool result)
        {
            _mockGeralRepository.Setup(r => r.SaveChangesAsync()).ReturnsAsync(result);
        }

        private void SetupGeralRepositorySaveChangesAsyncException(Exception exception)
        {
            _mockGeralRepository.Setup(r => r.SaveChangesAsync()).ThrowsAsync(exception);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted(); // Excluir o banco de dados após os testes
            GC.SuppressFinalize(this);
            _context.Dispose();
        }
    }
}
