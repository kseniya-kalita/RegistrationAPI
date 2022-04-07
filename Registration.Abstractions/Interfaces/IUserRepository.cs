using Registration.Abstractions.DbEntities;
using Registration.Abstractions.Models;

namespace Registration.Abstractions.Interfaces
{
    public interface IUserRepository
    {
        Task CreateAsync(UserDbEntity user);
        Task<List<UserDbEntity>> GetAllWithCompaniesAsync();
    }
}
