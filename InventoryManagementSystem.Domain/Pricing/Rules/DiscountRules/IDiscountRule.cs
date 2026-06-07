using InventoryManagementSystem.Domain.Customers;
using InventoryManagementSystem.Domain.Orders;

namespace InventoryManagementSystem.Domain.Pricing.Rules.DiscountRules
{
    public interface IDiscountRule
    {
        decimal Calculate(PricingContext context, decimal subtotal);
    }
}
