using Domain.AggregatesModel.UserAggragate;
using Domain.SeedWork;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Registration.DAL.Repositories
{
    /// <summary>
    /// Repository to manage company entity
    /// </summary>
    public class CompanyRepository : ICompanyRepository
    {
        private readonly ApplicationContext _context;

        public CompanyRepository(ApplicationContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<Company> AddAsync(Company company)
        {
            return (await _context.Companies.AddAsync(company)).Entity;
        }

        public async Task<Company> GetByIdAsync(Guid companyId)
        {
            return await _context.Companies
                .Include(_ => _.Users)
                .FirstOrDefaultAsync(c => c.Id == companyId);
        }

        public Company GetByName(string companyName)// ToDO K; redo
        {
            return _context.Companies
                .Include(_ => _.Users)
                .FirstOrDefault(c => string.Equals(c.Name, companyName));
        }
    }
}
