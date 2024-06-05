using Bsd.Domain.Entities;
using Bsd.Domain.Enums;
using Bsd.Domain.Repository.Interfaces;
using Bsd.Domain.Services.Interfaces;
using Bsd.Infrastructure.Context;
using Bsd.Infrastructure.RepositoryImpl;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace test.UnitTest.Infrastructure.Rpository
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
            var bsd = new BsdEntity
            {
                BsdNumber = 1234,
                DateService = DateTime.Today,
                Employees = new List<Employee>()
            };
            _mockDayTypeChecker.Setup(d => d.GetDayType(bsd.DateService)).Returns(DayType.HoliDay);
            _mockGeralRepository.Setup(r => r.SaveChangesAsync()).ReturnsAsync(true);

            // Act
            var result = await _bsdRepository.CreateBsdAsync(bsd);

            // Assert
            Assert.True(result);
            _mockGeralRepository.Verify(r => r.Create(It.IsAny<BsdEntity>()), Times.Once);
            _mockGeralRepository.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task CreateBsdAsync_ReturnDbUpdateException_WhenDbUpdateException()
        {
            // Arrange
            var bsd = new BsdEntity
            {
                BsdNumber = 1234,
                DateService = DateTime.Today,
                Employees = new List<Employee>()
            };
            _mockDayTypeChecker.Setup(d => d.GetDayType(bsd.DateService)).Returns(DayType.Sunday);
            _mockGeralRepository.Setup(r => r.SaveChangesAsync()).ThrowsAsync(new DbUpdateException());

            // Act & Assert
            await Assert.ThrowsAsync<DbUpdateException>(() => _bsdRepository.CreateBsdAsync(bsd));
        }

        [Fact]
        public async Task CreateBsdAsync_ReturnException_WhenGenericException()
        {
            // Arrange
            var bsd = new BsdEntity
            {
                BsdNumber = 1234,
                DateService = DateTime.Today,
                Employees = new List<Employee>()
            };
            _mockDayTypeChecker.Setup(d => d.GetDayType(bsd.DateService)).Throws(new Exception());

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _bsdRepository.CreateBsdAsync(bsd));
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted(); // Excluir o banco de dados após os testes
            _context.Dispose();
        }
    }
}