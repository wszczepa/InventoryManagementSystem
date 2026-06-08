namespace InventoryManagementSystem.Domain.Products
{
    public class Product
    {
        private Product()
        {
        }

        public Product(
            string name,
            string description,
            decimal price,
            int stock)
        {
            Name = name;
            Description = description;
            Price = price;
            Stock = stock;
        }

        public int ProductId { get; private set; }

        public string Name { get; private set; }

        public string Description { get; private set; }

        public decimal Price { get; private set; }

        public int Stock { get; private set; }

        public void Reserve(int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero.", nameof(quantity));

            if (Stock < quantity)
                throw new ArgumentException($"Not enough stock for product {ProductId}");

            Stock -= quantity;
        }

        public void Restock(int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero.", nameof(quantity));

            Stock += quantity;
        }

    }
}
