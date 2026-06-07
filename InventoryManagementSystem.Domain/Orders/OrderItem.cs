using InventoryManagementSystem.Domain.Products;

namespace InventoryManagementSystem.Domain.Orders
{
    public class OrderItem
    {
        private OrderItem()
        {
        }

        internal OrderItem(int productId, int quantity, decimal baseUnitPrice, decimal finalUnitPrice)
        {

            ProductId = productId;
            Quantity = quantity;
            BaseUnitPrice = baseUnitPrice;
            FinalUnitPrice = finalUnitPrice;
        }

        public int OrderItemId { get; private set; }

        public int OrderId { get; private set; }

        public int ProductId { get; private set; }

        public int Quantity { get; private set; }

        public decimal BaseUnitPrice { get; private set; }
        public decimal FinalUnitPrice { get; private set; }

        public Order Order { get; private set; } = null!;
        public Product Product { get; private set; } = null!;

    }
}
