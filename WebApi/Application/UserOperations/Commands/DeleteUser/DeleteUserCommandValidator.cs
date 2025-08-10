using FluentValidation;

namespace WebApi.Application.UserOperations.Commands.DeleteUser
{
    public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserCommandValidator()
        {
            RuleFor(command => command.UserId)
                .GreaterThan(0).WithMessage("Kullanıcı ID 0'dan büyük olmalıdır.");
        }
    }
}
