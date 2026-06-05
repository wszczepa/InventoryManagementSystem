using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryManagementSystem.Domain.Products
{
    public interface IProductRepository
    {
        Task AddAsync(Product product);

        Task<IEnumerable<Product>> GetAllAsync();

    }
}
