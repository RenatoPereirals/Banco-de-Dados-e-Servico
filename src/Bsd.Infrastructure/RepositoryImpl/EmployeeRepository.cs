using Bsd.Domain.Entities;
using Bsd.Domain.Repository.Interfaces;
using Bsd.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Bsd.Infrastructure.RepositoryImpl
{
    public class EmployeeRepository : GeralRepository, IEmployeeRepository
    {
        private readonly BsdDbContext _context;
        public EmployeeRepository(BsdDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Employee>> GetAllEmployees()
        {
            var query = GetEmployeeQuery();
            var queryString = query.ToQueryString();
            Console.WriteLine(queryString);
            return await query.ToListAsync();
        }

        public async Task<Employee> GetEmployeeByRegistrationAsync(int registration)
        {
            return await GetEmployeeQuery()
                .FirstOrDefaultAsync(e => e.EmployeeId == registration)
                    ?? throw new InvalidOperationException($"Funcionário com o matrícula {registration} não encontrado.");
        }

        private IQueryable<Employee> GetEmployeeQuery()
        {
            return _context.Employees
                .AsNoTracking()
                .Include(e => e.BsdEntities)
                .OrderBy(e => e.EmployeeId);
        }
    }
}