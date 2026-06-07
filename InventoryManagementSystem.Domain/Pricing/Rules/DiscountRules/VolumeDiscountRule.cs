using InventoryManagementSystem.Domain.Customers;
using InventoryManagementSystem.Domain.Orders;

namespace InventoryManagementSystem.Domain.Pricing.Rules.DiscountRules
{
    public class VolumeDiscountRule : IDiscountRule
    {
        public decimal Calculate(PricingContext context, decimal subtotal)
        {
            var qty = context.Order.Items.Sum(x => x.Quantity);

            if (qty >= 50)
                return subtotal * 0.30m;

            if (qty >= 10)
                return subtotal * 0.20m;

            if (qty >= 5)
                return subtotal * 0.10m;

            return 0;
        }
    }
}
