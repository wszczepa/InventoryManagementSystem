namespace InventoryManagementSystem.Api.Contracts.Products
{
    public sealed record OrderItemRequest(int ProductId, int Quantity);
}
