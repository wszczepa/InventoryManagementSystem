using InventoryManagementSystem.Domain.Orders;

namespace InventoryManagementSystem.Domain.Products
{
    public interface IProductRepository
    {
        Task CreateAsync(Product product);
        Task<IEnumerable<Product>> GetAllAsync();

        Task<IEnumerable<Product>> GetAsync(IEnumerable<int> ids);
    }
}
