using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Registration.Abstractions.Dtos;
using Registration.Abstractions.Dtos.Responses;
using Registration.Abstractions.Interfaces;
using RegistrationAPI.Controllers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Registration.Tests.Controllers
{
    public class UsersControllerTests
    {
        private readonly Mock<IUserManagementService> _mockedService;
        private readonly UsersController _controller;
        private readonly Fixture _fixture = new Fixture();

        public UsersControllerTests()
        {
            _mockedService = new Mock<IUserManagementService>();
            _controller = new UsersController(_mockedService.Object);
        }

        [Fact]
        public async Task CreateUserWithCompany_WhenObjectIsNull_ReturnsBadRequest()
        {
            UserForCreationDto userDto = null;

            var result = await _controller.CreateUserWithCompany(userDto) as BadRequestObjectResult;
        
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
            result.Value.Should().Be("Model can't be null.");
        }

        [Fact]
        public async Task CreateUserWithCompany_WithInvalidObject_ReturnsBadRequestResult()
        {
            var userDto = CreateUserDto(null);

            _controller.ModelState.AddModelError("Password", "'Password' cannot be null.");

            var badRequestResult = await _controller.CreateUserWithCompany(userDto) as BadRequestObjectResult;

            badRequestResult.Should().NotBeNull();

            badRequestResult.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }

        [Fact]
        public async Task CreateUserWithCompany_WithValidObject_ReturnsObjectResult()
        {
            var userDto = CreateUserDto();
            var expectedUser = new UserViewModel
            {
                Email = userDto.Email,
                Password = userDto.Password,
                Company = new CompanyViewModel
                {
                    Name = userDto.Company.Name
                }
            };

            _mockedService.Setup(x => x.CreateWithCompanyAsync(It.IsAny<UserForCreationDto>()))
                .ReturnsAsync(expectedUser);

            var result = await _controller.CreateUserWithCompany(userDto) as ObjectResult;

            result.Should().NotBeNull();

            result.StatusCode.Should().Be(StatusCodes.Status201Created);

            var response = result.Value as UserViewModel;
            response.Should().BeEquivalentTo(expectedUser, options =>
                    options.Excluding(x => x.Company.Id)
                           .Excluding(x => x.Id)
            );
        }

        [Fact]
        public async Task GetUsersWithCompanies_ReturnsOkObjectResult()
        {
            var expectedList = CreateUsersList();

            _mockedService.Setup(x => x.GetAllWithCompaniesAsync())
                .ReturnsAsync(expectedList);

            var okResult = await _controller.GetUsersWithCompanies() as OkObjectResult;

            okResult.Should().NotBeNull();

            okResult.StatusCode.Should().Be(StatusCodes.Status200OK);

            okResult.Value.Should().BeEquivalentTo(expectedList);
        }

        private List<UserViewModel> CreateUsersList()
        {
            return _fixture.Build<UserViewModel>()
                .CreateMany(2)
                .ToList();
        }

        private UserForCreationDto CreateUserDto(string password = "password")
        {
            return _fixture.Build<UserForCreationDto>()
                .With(u => u.Password, password)
                .Create();
        }
    }
}
