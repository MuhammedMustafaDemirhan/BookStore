using System;
using FluentAssertions;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using Xunit;

namespace Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandValidatorTests
    {
        [Theory]
        [InlineData("", "Surname", "2000-01-01")]    // name boş
        [InlineData("Na", "", "2000-01-01")]         // surname boş
        [InlineData("N", "Surname", "2000-01-01")]   // name kısa
        [InlineData("Name", "S", "2000-01-01")]      // surname kısa
        [InlineData("Name", "Surname", "3000-01-01")] // doğum tarihi gelecek
        public void WhenInvalidInputsAreGiven_Validator_ShouldReturnErrors(string name, string surname, string birthdate)
        {
            // Arrange
            CreateAuthorCommand command = new CreateAuthorCommand(null, null);
            command.Model = new CreateAuthorModel
            {
                Name = name,
                Surname = surname,
                DateOfBirth = DateTime.Parse(birthdate)
            };

            // Act
            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            var result = validator.Validate(command);

            // Assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotReturnError()
        {
            // Arrange
            CreateAuthorCommand command = new CreateAuthorCommand(null, null);
            command.Model = new CreateAuthorModel
            {
                Name = "ValidName",
                Surname = "ValidSurname",
                DateOfBirth = new DateTime(1980, 1, 1)
            };

            // Act
            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            var result = validator.Validate(command);

            // Assert
            result.Errors.Count.Should().Be(0);
        }
    }
}
