using ECommerceAPI.Application.Features.Commands.AppUser.CreateUser;
using ECommerceAPI.Application.Features.Commands.Product.CreateProduct;
using FluentValidation;

namespace ECommerceAPI.Application.Validators
{
    public class CreateProductValidator : AbstractValidator<CreateProductCommandRequest>
    {
        public CreateProductValidator()
        {
            RuleFor(v => v.Name)
                .NotEmpty()
                .WithMessage("Name can not be empty!")
                .NotNull().
                 WithMessage("Name can not be empty!").
                 MaximumLength(150).
                 WithMessage("Product name must be number between 3-150.")
                 .MinimumLength(3).
                 WithMessage("Product name must be number between 3-150.");

            RuleFor(p => p.Stock)
                .NotEmpty()
                .WithMessage("Stock can not be empty!")
                .NotNull()
                .WithMessage("Stock can not be empty!")
                .Must(s => s >= 0)
                .WithMessage("Stock cannot be negative!");

            RuleFor(p => p.Price)
                .NotEmpty()
                .WithMessage("Price can not be empty!")
                 .NotNull()
                .WithMessage("Price can not be empty!")
                .Must(s => s >= 0)
                .WithMessage("Price cannot be negative!");
        }
    }

}
