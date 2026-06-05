using InventoryManagementSystem.Domain.Orders;

namespace InventoryManagementSystem.Domain.Products
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }
        public int Stock { get; set; }

        public IEnumerable<OrderItem> OrderItems { get; set; } = [];
    }
}
