using Registration.Abstractions.Models;

namespace Registration.Abstractions.Interfaces
{
    public interface ICompanyRepository
    {
        Company GetByName(string name);
    }
}
