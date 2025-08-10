using FluentValidation.TestHelper;
using TestSetup;
using WebApi.Application.UserOperations.Queries.GetUserDetail;
using Xunit;

namespace Application.UserOperations.Queries.GetUserDetail
{
    public class GetUserDetailQueryValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void WhenUserIdIsInvalid_Validator_ShouldHaveError(int userId)
        {
            // Arrange
            GetUserDetailQuery query = new GetUserDetailQuery(null, null);
            query.UserId = userId;

            GetUserDetailQueryValidator validator = new GetUserDetailQueryValidator();

            // Act
            var result = validator.TestValidate(query);

            // Assert
            result.ShouldHaveValidationErrorFor(q => q.UserId);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        public void WhenUserIdIsValid_Validator_ShouldNotHaveError(int userId)
        {
            // Arrange
            GetUserDetailQuery query = new GetUserDetailQuery(null, null);
            query.UserId = userId;

            GetUserDetailQueryValidator validator = new GetUserDetailQueryValidator();

            // Act
            var result = validator.TestValidate(query);

            // Assert
            result.ShouldNotHaveValidationErrorFor(q => q.UserId);
        }
    }
}
