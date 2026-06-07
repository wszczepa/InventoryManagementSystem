using InventoryManagementSystem.Domain.Common;
using InventoryManagementSystem.Domain.Customers;
using InventoryManagementSystem.Domain.Orders;

namespace InventoryManagementSystem.Domain.Pricing.Rules.DiscountRules
{
    public class BlackFridayDiscountRule : IDiscountRule
    {
        public decimal Calculate(PricingContext context, decimal subtotal)
        {
            var d = context.Now;

            bool isBlackFriday = d.Month == 11 && d.Day >= 24 && d.Day <= 30;

            if (!isBlackFriday)
                return 0;

            return subtotal * 0.25m;
        }
    }
}
