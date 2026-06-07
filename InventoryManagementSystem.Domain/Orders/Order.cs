namespace InventoryManagementSystem.Domain.Orders
{
    public class Order
    {
        public int OrderId { get; set; }    

        public int CustomerId { get; set; }

        public IEnumerable<OrderItem> Items { get; set; } = [];
    }
}
