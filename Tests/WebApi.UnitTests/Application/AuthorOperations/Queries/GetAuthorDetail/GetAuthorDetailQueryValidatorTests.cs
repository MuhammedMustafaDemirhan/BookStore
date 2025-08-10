using FluentAssertions;
using TestSetup;
using WebApi.Application.AuthorOperations.Queries.GetAuthorDetail;
using Xunit;

namespace Application.AuthorOperations.Queries.GetAuthorDetail
{
    public class GetAuthorDetailQueryValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void WhenInvalidAuthorIdIsGiven_Validator_ShouldReturnError(int authorId)
        {
            // Arrange
            var query = new GetAuthorDetailQuery(null, null);
            query.AuthorId = authorId;

            var validator = new GetAuthorDetailQueryValidator();

            // Act
            var result = validator.Validate(query);

            // Assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidAuthorIdIsGiven_Validator_ShouldNotReturnError()
        {
            // Arrange
            var query = new GetAuthorDetailQuery(null, null);
            query.AuthorId = 1;

            var validator = new GetAuthorDetailQueryValidator();

            // Act
            var result = validator.Validate(query);

            // Assert
            result.Errors.Count.Should().Be(0);
        }
    }
}
