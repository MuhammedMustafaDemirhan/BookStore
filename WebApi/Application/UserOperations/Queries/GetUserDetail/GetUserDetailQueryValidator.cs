using FluentValidation;

namespace WebApi.Application.UserOperations.Queries.GetUserDetail
{
    public class GetUserDetailQueryValidator : AbstractValidator<GetUserDetailQuery>
    {
        public GetUserDetailQueryValidator()
        {
            RuleFor(query => query.UserId)
                .GreaterThan(0).WithMessage("Kullanıcı ID 0'dan büyük olmalıdır.");
        }
    }
}