using Registration.Abstractions.Models;

namespace Registration.Abstractions.Interfaces
{
    public interface IUserRepository
    {
        Task CreateAsync(User user);
        Task<List<User>> GetAllWithCompaniesAsync();
    }
}
