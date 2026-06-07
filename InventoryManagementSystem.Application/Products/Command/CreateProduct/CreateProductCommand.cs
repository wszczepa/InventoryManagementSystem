using InventoryManagementSystem.Application.Shared.Messaging;

namespace InventoryManagementSystem.Application.Products.Command.CreateProduct
{
    public sealed record CreateProductCommand(string Name, string Description, decimal Price, int Stock) : ICommand<int>;
}
