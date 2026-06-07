using InventoryManagementSystem.Application.Shared.Messaging;

namespace InventoryManagementSystem.Application.Orders.Command
{
    public record CreateOrderCommand(int CustomerId, IEnumerable<OrderItem> Items) : ICommand<int>;
}
