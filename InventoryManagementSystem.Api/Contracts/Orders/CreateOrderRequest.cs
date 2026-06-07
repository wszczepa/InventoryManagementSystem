using InventoryManagementSystem.Api.Contracts.Products;

namespace InventoryManagementSystem.Api.Contracts.Orders
{
    public sealed record CreateOrderRequest(int CustomerId, IEnumerable<OrderItemRequest> Items);

}
