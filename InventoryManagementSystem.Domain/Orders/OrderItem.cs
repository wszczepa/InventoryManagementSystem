using InventoryManagementSystem.Domain.Exceptions;
using InventoryManagementSystem.Domain.Products;

namespace InventoryManagementSystem.Domain.Orders
{
    public class OrderItem
    {
        private OrderItem()
        {
        }

        internal OrderItem(int productId, int quantity)
        {
            if (productId <= 0)
                throw new DomainException("Invalid product id.");

            if (quantity <= 0)
                throw new DomainException("Quantity must be greater than 0.");

            ProductId = productId;
            Quantity = quantity;
        }

        public int OrderItemId { get; private set; }

        public int OrderId { get; private set; }

        public int ProductId { get; private set; }

        public int Quantity { get; private set; }

        public Order Order { get; private set; } = null!;
        public Product Product { get; private set; } = null!;

    }
}
