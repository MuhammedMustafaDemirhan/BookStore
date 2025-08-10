using FluentValidation;

namespace WebApi.Application.UserOperations.Commands.UpdateUser
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(command => command.UserId).GreaterThan(0);

            RuleFor(command => command.Model.Name)
                .NotEmpty().WithMessage("İsim boş olamaz.")
                .MinimumLength(2).WithMessage("İsim en az 2 karakter olmalı.");

            RuleFor(command => command.Model.Surname)
                .NotEmpty().WithMessage("Soyisim boş olamaz.")
                .MinimumLength(2).WithMessage("Soyisim en az 2 karakter olmalı.");

            RuleFor(command => command.Model.Email)
                .NotEmpty().WithMessage("Email boş olamaz.")
                .EmailAddress().WithMessage("Geçerli bir email adresi girin.");

            RuleFor(command => command.Model.Password)
                .NotEmpty().WithMessage("Şifre boş olamaz.")
                .MinimumLength(4).WithMessage("Şifre en az 4 karakter olmalı.");
        }
    }
}