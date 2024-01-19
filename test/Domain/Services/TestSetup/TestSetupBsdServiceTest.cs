using Bsd.Domain.Entities;
using Bsd.Domain.Repository.Interfaces;
using Bsd.Domain.Services;
using Moq;

namespace test.Domain.Services.TestSetup
{
    public class TestSetupBsdServiceTest
    {
        public BsdEntity Bsd { get; private set; }
        public Mock<IEmployeeRepository> EmployeeRepository { get; private set; }
        public Mock<IRubricRepository> RubricRepository { get; private set; }

        public TestSetupBsdServiceTest()
        {
            Bsd = new BsdEntity(54321, DateTime.Now);
            EmployeeRepository = new Mock<IEmployeeRepository>();
            RubricRepository = new Mock<IRubricRepository>();
        }

        public async Task<Dictionary<int, List<Rubric>>> SetupAndAct(List<Rubric> listRubrics)
        {
            RubricRepository.Setup(r => r.GetAllRubricsAsync()).ReturnsAsync(listRubrics);

            var bsdService = new BsdService(RubricRepository.Object, EmployeeRepository.Object);
            var result = await bsdService.FilterRubricsByServiceTypeAndDayAsync(Bsd);

            return result;
        }
    }
}