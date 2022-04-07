using Domain.AggregatesModel.UserAggragate;
using FluentAssertions;
using System;
using Xunit;

namespace Registration.Tests.Domain.AggregatedModels
{
    public class UserDomainTests
    {
        [Theory]
        [InlineData(null, null)]
        [InlineData("email", null)]
        [InlineData(null, "password")]
        public void CreateUser_EmptyInputs_ShouldThrowArgumentNullException(string email, string password)
        {
            // Arrange

            // Act
            var act = () => { new User(email, password); };

            // Assert
            Assert.Throws<ArgumentNullException>(act);
        }

        [Fact]
        public void CreateUser_ValidInput_ShouldCreateDomain()
        {
            // Arrange

            // Act
            var userDomain = new User("userEmail", "userPassword");

            // Assert
            userDomain.Should().NotBeNull();
            userDomain.Should().BeEquivalentTo(new User("userEmail", "userPassword"));
        }

        [Fact]
        public void GetUserEmail_ShouldReturnValue()
        {
            // Arrange
            var userEmail = "userEmail";
            var userDomain = new User(userEmail, "userPassword");

            // Act
            var actualEmail = userDomain.GetUserEmail();

            // Assert
            actualEmail.Should().BeEquivalentTo(userEmail);
        }

        [Fact]
        public void GetUserPassword_ShouldReturnValue()
        {
            // Arrange
            var userPassword = "userPassword";
            var userDomain = new User("userEmail", userPassword);

            // Act
            var actualPassword = userDomain.GetUserPassword();

            // Assert
            actualPassword.Should().BeEquivalentTo(userPassword);
        }
    }
}
