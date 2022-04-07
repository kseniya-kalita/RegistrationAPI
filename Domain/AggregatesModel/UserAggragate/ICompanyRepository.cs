using Domain.SeedWork;

namespace Domain.AggregatesModel.UserAggragate
{
    public interface ICompanyRepository : IRepository<Company>
    {
        Task<Company> AddAsync(Company company);

        Company GetByName(string companyName);

        Task<Company> GetByIdAsync(Guid companyId);
    }
}
