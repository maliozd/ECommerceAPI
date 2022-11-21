using ECommerceAPI.Application.Features.Commands.AppUser.CreateUser;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Validators
{
    public class CreateUserValidator : AbstractValidator<CreateUserCommandRequest>
    {
        public CreateUserValidator()
        {
            RuleFor(u => u.Email)
                .NotNull()
                .WithMessage("Email can't be empty!")
                .EmailAddress();
            RuleFor(u => u.Username)
                .NotNull()
                .NotEmpty()
                .WithMessage("Username can't be empty")
                .MinimumLength(3)
                .WithMessage("Username length must be between 4-20 character ").
                MaximumLength(30);
            RuleFor(u => u.Password)
                .NotNull()
                .NotEmpty()
                .MinimumLength(6)
                .WithMessage("Password length must be between 6-30 character")
                .MaximumLength(30)
                .WithMessage("Password length must be between 6-30 character");
            RuleFor(u => u.PasswordConfirm)
                .NotNull().WithMessage("Please re-enter the password")
                .NotEmpty().WithMessage("Please re-enter the password")
                .Equal(u => u.Password).WithMessage("Passwords are not matching");
        }
    }
}
