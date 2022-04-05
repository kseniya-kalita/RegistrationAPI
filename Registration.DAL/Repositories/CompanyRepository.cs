using Registration.Abstractions.Interfaces;
using Registration.Abstractions.Models;

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

        /// <summary>
        /// Method to get company entity by it's name
        /// </summary>
        public Company GetByName(string name)
        {
            return _context.Companies.FirstOrDefault(c => c.Name == name);
        }
    }
}
