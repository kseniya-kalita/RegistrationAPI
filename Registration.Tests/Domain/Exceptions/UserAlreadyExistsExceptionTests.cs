using Domain.Exceptions;
using FluentAssertions;
using Xunit;

namespace Registration.Tests.Domain.Exceptions
{
    public class UserAlreadyExistsExceptionTests
    {
        [Fact]
        public void CreateException_ValidParams_ShouldConstructMessage()
        {
            // Arrange
            var companyName = "companyName";
            var userEmail = "userEmail";

            // Act
            var exception = new UserAlreadyExistsException(companyName, userEmail);

            // Assert
            exception.Should().NotBeNull();
            exception.Message.Should().BeEquivalentTo($"Company with name [companyName] already contains a user with email [userEmail]!");
        }
    }
}
