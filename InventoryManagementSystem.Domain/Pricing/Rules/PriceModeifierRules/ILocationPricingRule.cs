using InventoryManagementSystem.Domain.Customers;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryManagementSystem.Domain.Pricing.Rules.PriceModeifierRules
{
    public interface ILocationPricingRule
    {
        decimal Apply(CustomerRegion region, decimal currentPrice);
    }
}
