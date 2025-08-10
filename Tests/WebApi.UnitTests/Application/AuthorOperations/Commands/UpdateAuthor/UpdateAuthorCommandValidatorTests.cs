using System;
using FluentAssertions;
using TestSetup;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthor;
using Xunit;

namespace Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData("", "Surname", "1980-01-01")]
        [InlineData("N", "Surname", "1980-01-01")]
        [InlineData("Name", "", "1980-01-01")]
        [InlineData("Name", "S", "1980-01-01")]
        [InlineData("Name", "Surname", "3000-01-01")] // gelecekte bir tarih
        public void WhenInvalidInputsAreGiven_Validator_ShouldReturnErrors(string name, string surname, string dob)
        {
            // Arrange
            var command = new UpdateAuthorCommand(null);
            command.Model = new UpdateAuthorCommand.UpdateAuthorModel
            {
                Name = name,
                Surname = surname,
                DateOfBirth = DateTime.Parse(dob)
            };

            // Act
            var validator = new UpdateAuthorCommandValidation();
            var result = validator.Validate(command);

            // Assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotReturnErrors()
        {
            // Arrange
            var command = new UpdateAuthorCommand(null);
            command.Model = new UpdateAuthorCommand.UpdateAuthorModel
            {
                Name = "ValidName",
                Surname = "ValidSurname",
                DateOfBirth = new DateTime(1980, 1, 1)
            };

            // Act
            var validator = new UpdateAuthorCommandValidation();
            var result = validator.Validate(command);

            // Assert
            result.Errors.Count.Should().Be(0);
        }
    }
}
