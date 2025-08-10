using FluentValidation.TestHelper;
using TestSetup;
using WebApi.Application.UserOperations.Commands.UpdateUser;
using Xunit;

namespace Application.UserOperations.Commands.UpdateUser
{
    public class UpdateUserCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void WhenUserIdIsInvalid_Validator_ShouldHaveError(int userId)
        {
            var command = new UpdateUserCommand(null);
            command.UserId = userId;
            command.Model = new UpdateUserModel
            {
                Name = "ValidName",
                Surname = "ValidSurname",
                Email = "valid@example.com",
                Password = "validpass"
            };

            var validator = new UpdateUserCommandValidator();
            var result = validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(c => c.UserId);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("a")]
        public void WhenInvalidNameIsGiven_Validator_ShouldHaveError(string name)
        {
            var command = new UpdateUserCommand(null);
            command.UserId = 1;
            command.Model = new UpdateUserModel
            {
                Name = name,
                Surname = "ValidSurname",
                Email = "valid@example.com",
                Password = "validpass"
            };

            var validator = new UpdateUserCommandValidator();
            var result = validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(c => c.Model.Name);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("a")]
        public void WhenInvalidSurnameIsGiven_Validator_ShouldHaveError(string surname)
        {
            var command = new UpdateUserCommand(null);
            command.UserId = 1;
            command.Model = new UpdateUserModel
            {
                Name = "ValidName",
                Surname = surname,
                Email = "valid@example.com",
                Password = "validpass"
            };

            var validator = new UpdateUserCommandValidator();
            var result = validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(c => c.Model.Surname);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("invalidemail")]
        public void WhenInvalidEmailIsGiven_Validator_ShouldHaveError(string email)
        {
            var command = new UpdateUserCommand(null);
            command.UserId = 1;
            command.Model = new UpdateUserModel
            {
                Name = "ValidName",
                Surname = "ValidSurname",
                Email = email,
                Password = "validpass"
            };

            var validator = new UpdateUserCommandValidator();
            var result = validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(c => c.Model.Email);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("123")]
        public void WhenInvalidPasswordIsGiven_Validator_ShouldHaveError(string password)
        {
            var command = new UpdateUserCommand(null);
            command.UserId = 1;
            command.Model = new UpdateUserModel
            {
                Name = "ValidName",
                Surname = "ValidSurname",
                Email = "valid@example.com",
                Password = password
            };

            var validator = new UpdateUserCommandValidator();
            var result = validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(c => c.Model.Password);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotHaveErrors()
        {
            var command = new UpdateUserCommand(null);
            command.UserId = 1;
            command.Model = new UpdateUserModel
            {
                Name = "ValidName",
                Surname = "ValidSurname",
                Email = "valid@example.com",
                Password = "validpass"
            };

            var validator = new UpdateUserCommandValidator();
            var result = validator.TestValidate(command);

            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
