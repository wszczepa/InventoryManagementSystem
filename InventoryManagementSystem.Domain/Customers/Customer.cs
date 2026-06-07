using InventoryManagementSystem.Domain.Exceptions;

namespace InventoryManagementSystem.Domain.Customers
{
    public class Customer
    {
        private Customer()
        {
        }

        public Customer(string name, string lastName, CustomerRegion region)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Name is required");

            if (string.IsNullOrWhiteSpace(lastName))
                throw new DomainException("LastName is required");

            Name = name;
            LastName = lastName;
            Region = region;
        }

        public int CustomerId { get; private set; }

        public string Name { get; private set; } = null!;

        public string LastName { get; private set; } = null!;

        public CustomerRegion Region { get; private set; }
    }
}
