using FluentValidation.TestHelper;
using TestSetup;
using WebApi.Application.UserOperations.Commands.CreateUser;
using Xunit;

namespace Application.UserOperations.Commands.CreateUser
{
    public class CreateUserCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("a")]
        public void WhenInvalidNameIsGiven_Validator_ShouldHaveError(string name)
        {
            var command = new CreateUserCommand(null, null);
            command.Model = new CreateUserCommand.CreateUserModel
            {
                Name = name,
                Surname = "ValidSurname",
                Email = "valid@example.com",
                Password = "123456"
            };

            var validator = new CreateUserCommandValidator();
            var result = validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(c => c.Model.Name);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("a")]
        public void WhenInvalidSurnameIsGiven_Validator_ShouldHaveError(string surname)
        {
            var command = new CreateUserCommand(null, null);
            command.Model = new CreateUserCommand.CreateUserModel
            {
                Name = "ValidName",
                Surname = surname,
                Email = "valid@example.com",
                Password = "123456"
            };

            var validator = new CreateUserCommandValidator();
            var result = validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(c => c.Model.Surname);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("invalidemail")]
        public void WhenInvalidEmailIsGiven_Validator_ShouldHaveError(string email)
        {
            var command = new CreateUserCommand(null, null);
            command.Model = new CreateUserCommand.CreateUserModel
            {
                Name = "ValidName",
                Surname = "ValidSurname",
                Email = email,
                Password = "123456"
            };

            var validator = new CreateUserCommandValidator();
            var result = validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(c => c.Model.Email);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("123")]
        public void WhenInvalidPasswordIsGiven_Validator_ShouldHaveError(string password)
        {
            var command = new CreateUserCommand(null, null);
            command.Model = new CreateUserCommand.CreateUserModel
            {
                Name = "ValidName",
                Surname = "ValidSurname",
                Email = "valid@example.com",
                Password = password
            };

            var validator = new CreateUserCommandValidator();
            var result = validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(c => c.Model.Password);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotHaveErrors()
        {
            var command = new CreateUserCommand(null, null);
            command.Model = new CreateUserCommand.CreateUserModel
            {
                Name = "ValidName",
                Surname = "ValidSurname",
                Email = "valid@example.com",
                Password = "123456"
            };

            var validator = new CreateUserCommandValidator();
            var result = validator.TestValidate(command);

            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
