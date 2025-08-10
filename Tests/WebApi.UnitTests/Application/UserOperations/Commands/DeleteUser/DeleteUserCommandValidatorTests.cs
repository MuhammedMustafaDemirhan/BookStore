using FluentValidation.TestHelper;
using TestSetup;
using WebApi.Application.UserOperations.Commands.DeleteUser;
using Xunit;

namespace Application.UserOperations.Commands.DeleteUser
{
    public class DeleteUserCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void WhenUserIdIsInvalid_Validator_ShouldHaveError(int userId)
        {
            // Arrange
            DeleteUserCommand command = new DeleteUserCommand(null);
            command.UserId = userId;

            DeleteUserCommandValidator validator = new DeleteUserCommandValidator();

            // Act
            var result = validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(c => c.UserId);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        public void WhenUserIdIsValid_Validator_ShouldNotHaveError(int userId)
        {
            // Arrange
            DeleteUserCommand command = new DeleteUserCommand(null);
            command.UserId = userId;

            DeleteUserCommandValidator validator = new DeleteUserCommandValidator();

            // Act
            var result = validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveValidationErrorFor(c => c.UserId);
        }
    }
}
