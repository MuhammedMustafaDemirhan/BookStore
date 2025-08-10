using FluentAssertions;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using Xunit;
using TestSetup;

namespace Application.GenreOperations.Queries.GetGenreDetail
{
    public class GetGenreDetailQueryValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void WhenInvalidGenreIdIsGiven_Validator_ShouldReturnError(int genreId)
        {
            // Arrange
            var query = new GetGenreDetailQuery(null, null);
            query.GenreId = genreId;

            var validator = new GetGenreDetailQueryValidator();

            // Act
            var result = validator.Validate(query);

            // Assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidGenreIdIsGiven_Validator_ShouldNotReturnError()
        {
            // Arrange
            var query = new GetGenreDetailQuery(null, null);
            query.GenreId = 1;

            var validator = new GetGenreDetailQueryValidator();

            // Act
            var result = validator.Validate(query);

            // Assert
            result.Errors.Count.Should().Be(0);
        }
    }
}
