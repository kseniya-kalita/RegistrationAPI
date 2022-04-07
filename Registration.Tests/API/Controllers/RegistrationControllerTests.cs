using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RegistrationAPI.Boundary.Requests;
using RegistrationAPI.Boundary.Responses;
using RegistrationAPI.Controllers;
using RegistrationAPI.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Registration.Tests.Controllers
{
    public class RegistrationControllerTests
    {
        private readonly Mock<ICompanyManagementService> _mockedService;
        private readonly CompaniesController _controller;
        private readonly Fixture _fixture = new Fixture();

        public RegistrationControllerTests()
        {
            _mockedService = new Mock<ICompanyManagementService>();
            _controller = new CompaniesController(_mockedService.Object);
        }

        [Fact]
        public async Task RegisterUserWithCompany_WhenObjectIsNull_ReturnsBadRequest()
        { 
            // Arrange
            UserForCreationDto userDto = null;

            // Act
            var result = await _controller.RegisterUserWithCompany(userDto) as BadRequestObjectResult;
        
            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
            result.Value.Should().Be("Model can't be null.");
        }

        [Fact]
        public async Task CreateUserWithCompany_WithInvalidObject_ReturnsBadRequestResult()
        {
            // Arrange
            var userDto = _fixture.Build<UserForCreationDto>()
                .With(u => u.Password, (string)null)
                .Create();

            _controller.ModelState.AddModelError("Password", "'Password' cannot be null.");
            
            // Act
            var badRequestResult = await _controller.RegisterUserWithCompany(userDto) as BadRequestObjectResult;

            // Assert
            badRequestResult.Should().NotBeNull();

            badRequestResult.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }

        [Fact]
        public async Task CreateUserWithCompany_WithValidObject_ReturnsObjectResult()
        {
            // Arrange
            var userDto = _fixture.Create<UserForCreationDto>();

            var expectedCompanyDto = new CompanyDto()
            {
                Id = Guid.NewGuid(),
                Name = userDto.Company.Name,
                Users = new List<UserDto>
                {
                    new UserDto
                    {
                        Id = Guid.NewGuid(),
                        Email = userDto.Email,
                        Password = userDto.Password
                    }
                }
            };

            _mockedService.Setup(x => x.CreateAsync(It.IsAny<UserForCreationDto>()))
                .ReturnsAsync(expectedCompanyDto);

            // Act
            var result = await _controller.RegisterUserWithCompany(userDto) as ObjectResult;

            // Assert
            result.Should().NotBeNull();

            result.StatusCode.Should().Be(StatusCodes.Status201Created);

            var response = result.Value as CompanyDto;

            response.Should().BeEquivalentTo(expectedCompanyDto);
        }
    }
}
