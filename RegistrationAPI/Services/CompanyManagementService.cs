using Domain.AggregatesModel.UserAggragate;
using Domain.Exceptions;
using RegistrationAPI.Boundary.Requests;
using RegistrationAPI.Boundary.Responses;
using RegistrationAPI.Services;

namespace Registration.Services
{
    public class CompanyManagementService : ICompanyManagementService
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyManagementService(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        /// <summary>
        /// Method to check if provided company exists and if so create a user and assign this company to him,
        /// otherwise to create a new company and a new user for this company
        /// </summary>
        
        // In the task description i haven't found what to do in case if company with provided
        // name alread exists in database. If it was a real project i would ask the team about that.
        // In these solution i supposed that name is unique and check if it exists in databases. if so, i used 
        // existent company for the user, else - created a new one.
        public async Task<CompanyDto> CreateAsync(UserForCreationDto userDto)
        {
            if (userDto?.Company?.Name == null)
            {
                throw new ArgumentNullException(nameof(userDto));
            } 
            
            var company = _companyRepository.GetByName(userDto.Company.Name);
            if (company == null)
            {
                company = await _companyRepository.AddAsync(new Company(userDto.Company.Name));
            }
            company.AddUser(userDto.Email, userDto.Password);

            await _companyRepository.UnitOfWork.SaveChangesAsync();

            return CompanyDto.FromCompany(company);
        }

        /// <summary>
        /// Method to get all companies 
        /// </summary>
        public CompanyDto GetByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }
            var company = _companyRepository.GetByName(name);
           
            if (company == null)
            {
                throw new EntityNotFoundException(nameof(Company));
            }

            return CompanyDto.FromCompany(company);
        }
    }
}
