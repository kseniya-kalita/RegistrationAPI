using Microsoft.AspNetCore.Mvc;
using Registration.Abstractions.Dtos;
using Registration.Abstractions.Dtos.Responses;
using Registration.Abstractions.Interfaces;

namespace RegistrationAPI.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserManagementService _userManagementService;
        
        public UsersController(IUserManagementService userManagementService)
        {
            _userManagementService = userManagementService;
        }

        /// <summary>
        /// Endpoint to get users with their companies
        /// </summary>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<UserViewModel>))]
        [HttpGet]
        public async Task<IActionResult> GetUsersWithCompanies()
        {
            var users = await _userManagementService.GetAllWithCompaniesAsync();
            
            return Ok(users);
        }

        /// <summary>
        /// Endpoint to create a user and it's company (if company doesn't exist)
        ///       or to create a user and assign a company to him 
        /// </summary>
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UserViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<IActionResult> CreateUserWithCompany([FromBody] UserForCreationDto userDto)
        {
            if (userDto == null)
            {
                return BadRequest("Model can't be null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // In a real project i would use global error handler
            var user = await _userManagementService.CreateWithCompanyAsync(userDto);
            

            return new ObjectResult(user) { StatusCode = StatusCodes.Status201Created };
        }
    }
}