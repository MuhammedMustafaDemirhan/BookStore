using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;
using Xunit;

namespace Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData("abc")]
        public void WhenInvalidNameIsGiven_Validator_ShouldReturnError(string name)
        {
            // Arrange
            var command = new UpdateGenreCommand(null);
            command.Model = new UpdateGenreModel()
            {
                Name = name,
                IsActive = true
            };

            // Act
            var validator = new UpdateGenreCommandValidator();
            var result = validator.Validate(command);

            // Assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidNameIsGiven_Validator_ShouldNotReturnError()
        {
            // Arrange
            var command = new UpdateGenreCommand(null);
            command.Model = new UpdateGenreModel()
            {
                Name = "Science Fiction",
                IsActive = true
            };

            // Act
            var validator = new UpdateGenreCommandValidator();
            var result = validator.Validate(command);

            // Assert
            result.Errors.Count.Should().Be(0);
        }
    }
}
