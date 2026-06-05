using InventoryManagementSystem.Domain.Products;

namespace InventoryManagementSystem.Domain.Orders
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }

        public int OrderId { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
        public int Quantity { get; set; }
    }
}
