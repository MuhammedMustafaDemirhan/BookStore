using FluentValidation;
using System;

namespace WebApi.Application.UserOperations.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(command => command.Model.Name)
                .NotEmpty().WithMessage("İsim alanı boş olamaz.")
                .MinimumLength(2).WithMessage("İsim en az 2 karakter olmalıdır.");

            RuleFor(command => command.Model.Surname)
                .NotEmpty().WithMessage("Soyisim alanı boş olamaz.")
                .MinimumLength(2).WithMessage("Soyisim en az 2 karakter olmalıdır.");

            RuleFor(command => command.Model.Email)
                .NotEmpty().WithMessage("Email boş olamaz.")
                .EmailAddress().WithMessage("Geçerli bir email adresi girilmelidir.");

            RuleFor(command => command.Model.Password)
                .NotEmpty().WithMessage("Şifre boş olamaz.")
                .MinimumLength(6).WithMessage("Şifre en az 4 karakter olmalıdır.");
        }
    }
}