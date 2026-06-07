using InventoryManagementSystem.Domain.Exceptions;
using InventoryManagementSystem.Domain.Orders;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryManagementSystem.Domain.Customers
{
    public class Customer
    {
        private Customer()
        {
        }

        public Customer(string name, string lastName, string location)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Name is required");

            if (string.IsNullOrWhiteSpace(lastName))
                throw new DomainException("LastName is required");

            if (string.IsNullOrWhiteSpace(location))
                throw new DomainException("Location is required");

            Name = name;
            LastName = lastName;
            Location = location;
        }

        public int CustomerId { get; private set; }

        public string Name { get; private set; } = null!;

        public string LastName { get; private set; } = null!;

        public string Location { get; private set; } = null!;
    }
}
