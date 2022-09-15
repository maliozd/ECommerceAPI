using ECommerceAPI.Application.ViewModels.Products;
using FluentValidation;

namespace ECommerceAPI.Application.Validators
{
    public class CreateProductValidator : AbstractValidator<VM_Create_Product>
    {
        public CreateProductValidator()
        {
            RuleFor(v => v.Name)
                .NotEmpty()
                .NotNull().
                 WithMessage("Name can not be empty!").
                 MaximumLength(150).
                 MinimumLength(3).
                 WithMessage("Product name must be number between 3-150.");

            RuleFor(p => p.Stock)
                .NotEmpty().
                 NotNull()
                .WithMessage("Stock can not be empty!").Must(s => s >= 0).WithMessage("Stock cannot be negative!");

            RuleFor(p => p.Price)
                .NotEmpty().
                 NotNull()
                .WithMessage("Price can not be empty!").Must(s => s >= 0).WithMessage("Price cannot be negative!");
        }
    }
}
