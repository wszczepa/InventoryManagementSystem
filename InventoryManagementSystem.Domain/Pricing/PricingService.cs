using InventoryManagementSystem.Domain.Customers;
using InventoryManagementSystem.Domain.Orders;
using InventoryManagementSystem.Domain.Pricing.Rules.DiscountRules;
using InventoryManagementSystem.Domain.Pricing.Rules.PriceModeifierRules;

namespace InventoryManagementSystem.Domain.Pricing
{
    public class PricingService : IPricingService
    {

        private readonly IEnumerable<IDiscountRule> _discountRules;

        public PricingService(IEnumerable<IDiscountRule> discountRules)
        {
            _discountRules = discountRules;
        }
        public decimal CalculateBestDiscount(PricingContext context)
        {

            var subtotal = context.Order.Items.Sum(i => i.Quantity * i.FinalUnitPrice);

            return _discountRules
                .Select(d => d.Calculate(context, subtotal))
                .DefaultIfEmpty(0m)
                .Max();
        }
    }
}
