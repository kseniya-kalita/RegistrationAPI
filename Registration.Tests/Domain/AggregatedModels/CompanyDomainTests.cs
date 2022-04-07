using Domain.AggregatesModel.UserAggragate;
using Domain.Exceptions;
using FluentAssertions;
using System;
using Xunit;

namespace Registration.Tests.Domain.AggregatedModels
{
    public class CompanyDomainTests
    {
        [Fact]
        public void CreateCompany_EmptyName_ShouldThrowArgumentNullException()
        {
            // Arrange
            var companyName = string.Empty;

            // Act
            var act = () => { var companyDomain = new Company(companyName); };

            // Assert
            Assert.Throws<ArgumentNullException>(act);
        }

        [Fact]
        public void CreateCompany_ValidName_ShouldCreateDomain()
        {
            // Arrange
            var companyName = "companyName";

            // Act
            var companyDomain = new Company(companyName);

            // Assert
            companyDomain.Should().NotBeNull();
            companyDomain.Name.Should().Be(companyName);
            companyDomain.Users.Should().NotBeNull();
            companyDomain.Users.Should().HaveCount(0);

        }

        [Theory]
        [InlineData(null, null)]
        [InlineData("email", null)]
        [InlineData(null, "password")]
        public void AddUser_NullInput_ShouldThrowArgumentNullExceptio(string email, string password)
        {
            // Arrange
            var companyName = "companyName";
            var companyDomain = new Company(companyName);

            // Act
            var act = () => { companyDomain.AddUser(email, password); };

            // Assert
            Assert.Throws<ArgumentNullException>(act);

        }

        [Fact]
        public void AddUser_TryAddExistingUser_ShouldThrowUserAlreadyExistsException()
        {
            // Arrange
            var companyName = "companyName";
            var companyDomain = new Company(companyName);
            var userEmail = "userEmail";
            var userPassword = "userPassword";
            companyDomain.AddUser(userEmail, userPassword);

            // Act
            var act = () => { companyDomain.AddUser(userEmail, "OtherPassword"); };

            // Assert
            Assert.Throws<UserAlreadyExistsException>(act);
        }

        [Fact]
        public void AddUser_ValidInput_ShouldAddUser()
        {
            // Arrange
            var companyName = "companyName";
            var companyDomain = new Company(companyName);
            companyDomain.AddUser("userEmail", "userPassword");

            // Act
            companyDomain.AddUser("otherEmail", "OtherPassword");

            // Assert
            companyDomain.Should().NotBeNull();
            companyDomain.Name.Should().Be(companyName);

            companyDomain.Users.Should().NotBeNull();
            companyDomain.Users.Should().HaveCount(2);

            companyDomain.Users.Should().ContainEquivalentOf(new User("userEmail", "userPassword"));
            companyDomain.Users.Should().ContainEquivalentOf(new User("otherEmail", "OtherPassword"));

        }

        [Fact]
        public void CompanyName_ShouldReturnName()
        {
            // Arrange
            var companyName = "companyName";
            var companyDomain = new Company(companyName);

            // Act
            var actualName = companyDomain.Name;

            // Assert
            actualName.Should().BeEquivalentTo(companyName);
        }
    }
}
