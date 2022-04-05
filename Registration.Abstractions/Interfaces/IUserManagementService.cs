using Registration.Abstractions.Dtos;
using Registration.Abstractions.Dtos.Responses;

namespace Registration.Abstractions.Interfaces
{
    public interface IUserManagementService
    {
        Task<UserViewModel> CreateWithCompanyAsync(UserForCreationDto userDto);
        Task<List<UserViewModel>> GetAllWithCompaniesAsync();
    }
}
