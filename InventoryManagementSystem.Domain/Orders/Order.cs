using InventoryManagementSystem.Domain.Customers;
using InventoryManagementSystem.Domain.Exceptions;
using InventoryManagementSystem.Domain.Products;

namespace InventoryManagementSystem.Domain.Orders
{
    public class Order
    {
        private readonly List<OrderItem> _items = [];

        private Order()
        {
        }

        public Order(int customerId)
        {
            if (customerId <= 0)
                throw new DomainException("CustomerId must be greater than 0.");

            CustomerId = customerId;
        }

        public int OrderId { get; private set; }

        public int CustomerId { get; private set; }

        public Customer Customer { get; private set; } = null!;

        public IEnumerable<OrderItem> Items => _items;

        public void AddItem(int productId, int quantity)
        {
            if (productId <= 0)
                throw new DomainException("Invalid product id.");

            if (quantity <= 0)
                throw new DomainException("Quantity must be greater than 0.");

            _items.Add(new OrderItem(productId, quantity));
        }
    }
}
