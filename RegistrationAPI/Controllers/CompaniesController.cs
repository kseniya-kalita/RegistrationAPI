using Microsoft.AspNetCore.Mvc;
using RegistrationAPI.Boundary.Requests;
using RegistrationAPI.Boundary.Responses;
using RegistrationAPI.Services;

namespace RegistrationAPI.Controllers
{
    [ApiController]
    [Route("api/companies")]
    public class CompaniesController : ControllerBase
    {
        private readonly ICompanyManagementService _companyManagementService;
        
        public CompaniesController(ICompanyManagementService companyManagementService)
        {
            _companyManagementService = companyManagementService;
        }

        /// <summary>
        /// Endpoint to get users with their companies
        /// </summary>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<CompanyDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseErrorResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(BaseErrorResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(BaseErrorResponse))]
        [HttpGet]
        public IActionResult GetUsersWithCompanies(string companyName)
        {
            if (string.IsNullOrWhiteSpace(companyName))
            {
                return BadRequest("Company name can't be empty.");
            }
            var companyDto =  _companyManagementService.GetByName(companyName);

            return Ok(companyDto);
        }

        /// <summary>
        /// Endpoint to create a user and it's company (if company doesn't exist)
        ///       or to create a user and assign a company to him 
        /// </summary>
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CompanyDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(BaseErrorResponse))]
        [HttpPost]
        public async Task<IActionResult> RegisterUserWithCompany([FromBody] UserForCreationDto userDto)
        {
            if (userDto == null)
            {
                return BadRequest("Model can't be null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var companyDto = await _companyManagementService.CreateAsync(userDto);

            return new ObjectResult(companyDto) { StatusCode = StatusCodes.Status201Created };
        }
    }
}