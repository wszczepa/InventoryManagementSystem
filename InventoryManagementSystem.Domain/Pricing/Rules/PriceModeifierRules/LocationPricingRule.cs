using InventoryManagementSystem.Domain.Customers;

namespace InventoryManagementSystem.Domain.Pricing.Rules.PriceModeifierRules
{
    public class LocationPricingRule : ILocationPricingRule
    {
        public decimal Apply(CustomerRegion region, decimal currentPrice)
        {
            return region switch
            {
                CustomerRegion.US => currentPrice,
                CustomerRegion.Europe => currentPrice * 1.15m,
                CustomerRegion.Asia => currentPrice * 1.05m,
                _ => currentPrice
            };
        }
    }
}
