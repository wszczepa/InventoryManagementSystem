using InventoryManagementSystem.Application.Shared.Messaging;

namespace InventoryManagementSystem.Application.Products.Queries.GetAllProducts
{
    public sealed record GetAllProductsQuery : IQuery<IEnumerable<ProductDto>>;
}
