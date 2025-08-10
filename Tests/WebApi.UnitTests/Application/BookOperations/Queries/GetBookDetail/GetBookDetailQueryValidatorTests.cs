using FluentAssertions;
using WebApi.Application.BookOperations.Queries.GetBookDetail;
using Xunit;
using TestSetup;

namespace Application.BookOperations.Queries.GetBookDetail
{
    public class GetBookDetailQueryValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-5)]
        public void WhenInvalidBookIdIsGiven_Validator_ShouldReturnError(int bookId)
        {
            // Arrange
            var query = new GetBookDetailQuery(null, null);
            query.BookId = bookId;

            var validator = new GetBookDetailQueryValidation();

            // Act
            var result = validator.Validate(query);

            // Assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidBookIdIsGiven_Validator_ShouldNotReturnError()
        {
            // Arrange
            var query = new GetBookDetailQuery(null, null);
            query.BookId = 2;

            var validator = new GetBookDetailQueryValidation();

            // Act
            var result = validator.Validate(query);

            // Assert
            result.Errors.Count.Should().Be(0);
        }
    }
}
