using Domain.AggregatesModel.UserAggragate;
using RegistrationAPI.Boundary.Requests;
using RegistrationAPI.Boundary.Responses;

namespace RegistrationAPI.Services
{
    public interface ICompanyManagementService
    {
        Task<CompanyDto> CreateAsync(UserForCreationDto userDto);
        CompanyDto GetByName(string name);
    }
}
