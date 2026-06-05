using InventoryManagementSystem.Application.Shared.Messaging;

namespace InventoryManagementSystem.Application.Products.Command.AddProduct
{
    public sealed record AddProductCommand(string Name, string Description, decimal Price, int Stock) : ICommand<int>;
}
