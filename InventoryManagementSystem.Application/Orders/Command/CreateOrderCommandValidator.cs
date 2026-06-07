using FluentValidation;

namespace InventoryManagementSystem.Application.Orders.Command
{
    public class CreateOrderCommandValidator
        : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(x => x.CustomerId)
                .GreaterThan(0);

            RuleFor(x => x.Items)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.Items)
                .Must(items => items.Select(i => i.ProductId).Distinct().Count() == items.Count())
                .WithMessage("Duplicate products in order are not allowed.");

            RuleForEach(x => x.Items)
                .SetValidator(new OrderItemValidator());
        }
    }
}
