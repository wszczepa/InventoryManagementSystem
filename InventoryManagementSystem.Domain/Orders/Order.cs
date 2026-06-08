using InventoryManagementSystem.Domain.Customers;

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
                throw new ArgumentException("CustomerId must be greater than 0.");

            CustomerId = customerId;
        }

        public int OrderId { get; private set; }

        public int CustomerId { get; private set; }

        public Customer Customer { get; private set; } = null!;

        public IEnumerable<OrderItem> Items => _items;

        public decimal Subtotal { get; private set; }
        public decimal Discount { get; private set; }
        public decimal Total { get; private set; }

        public void AddItem(int productId, int quantity, decimal unitPrice, decimal finalPrice)
        {
            if (productId <= 0)
                throw new ArgumentException("Invalid product id.", nameof(productId));

            if (quantity <= 0)
                throw new ArgumentException("Quantity must be greater than 0.", nameof(quantity));

            if (unitPrice < 0)
                throw new ArgumentException("Unit price cannot be negative.", nameof(unitPrice));

            if (finalPrice < 0)
                throw new ArgumentException("Final price cannot be negative.", nameof(finalPrice));

            _items.Add(new OrderItem(productId, quantity, unitPrice, finalPrice));
        }

        public void ApplyPricing(decimal discount)
        {
            Subtotal = _items.Sum(i => i.Quantity * i.FinalUnitPrice);
            Discount = discount;
            Total = Subtotal - Discount;
        }
    }
}
