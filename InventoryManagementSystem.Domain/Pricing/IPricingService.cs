using InventoryManagementSystem.Domain.Customers;
using InventoryManagementSystem.Domain.Orders;

namespace InventoryManagementSystem.Domain.Pricing
{
    public interface IPricingService
    {
        decimal CalculateBestDiscount(PricingContext context);
    }

}
