using Application.AuthorOperations.Commands.DeleteAuthor;
using FluentAssertions;
using TestSetup;
using WebApi.Application.AuthorOperations.Commands.DeleteAuthor;
using Xunit;

namespace Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void WhenInvalidAuthorIdIsGiven_Validator_ShouldReturnError(int authorId)
        {
            // Arrange
            var command = new DeleteAuthorCommand(null)
            {
                AuthorId = authorId
            };

            // Act
            var validator = new DeleteAuthorCommandValidator();
            var result = validator.Validate(command);

            // Assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidAuthorIdIsGiven_Validator_ShouldNotReturnError()
        {
            // Arrange
            var command = new DeleteAuthorCommand(null)
            {
                AuthorId = 1
            };

            // Act
            var validator = new DeleteAuthorCommandValidator();
            var result = validator.Validate(command);

            // Assert
            result.Errors.Count.Should().Be(0);
        }
    }
}
