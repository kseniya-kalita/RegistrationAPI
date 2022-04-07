using AutoMapper;
using Registration.Abstractions.DbEntities;
using Registration.Abstractions.Dtos;
using Registration.Abstractions.Dtos.Responses;
using Registration.Abstractions.Interfaces;
using Registration.Abstractions.Models;

namespace Registration.Services
{
    public class UserManagementService : IUserManagementService
    {
        private readonly IUserRepository _userRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;

        public UserManagementService(IUserRepository userRepository, ICompanyRepository companyRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _companyRepository = companyRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Method to check if provided company exists and if so create a user and assign this company to him,
        /// otherwise to create a new company and a new user for this company
        /// </summary>
        
        // In the task description i haven't found what to do in case if company with provided
        // name alread exists in database. If it was a real project i would ask the team about that.
        // In these solution i supposed that name is unique and check if it exists in databases. if so, i used 
        // existent company for the user, else - created a new one.
        public async Task<UserViewModel> CreateWithCompanyAsync(UserForCreationDto userDto)
        {
            var company = _companyRepository.GetByName(userDto.Company.Name);
            
            User user = new User(Guid.Empty, 
                userDto.Email,
                userDto.Password,
                company?.Id ?? Guid.Empty, 
                userDto.Company.Name);

            var dbUser = _mapper.Map<UserDbEntity>(user);
            await _userRepository.CreateAsync(dbUser);
            user = _mapper.Map<User>(dbUser);
            var userViewModel = _mapper.Map<UserViewModel>(user);

            return userViewModel;
        }

        /// <summary>
        /// Method to get all companies 
        /// </summary>
        public async Task<List<UserViewModel>> GetAllWithCompaniesAsync()
        {
            var users = await _userRepository.GetAllWithCompaniesAsync();
            var userViewModels = _mapper.Map<List<UserViewModel>>(users);

            return userViewModels;
        }
    }
}
