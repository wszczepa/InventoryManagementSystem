using InventoryManagementSystem.Domain.Common;
using InventoryManagementSystem.Domain.Customers;
using InventoryManagementSystem.Domain.Orders;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryManagementSystem.Domain.Pricing
{
    public class PricingContext
    {
        public DateTime Now { get; init; }
        public Order Order { get; init; } = default!;
        public Customer Customer { get; init; } = default!;
    }
}
