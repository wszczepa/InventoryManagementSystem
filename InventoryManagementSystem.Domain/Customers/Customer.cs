using InventoryManagementSystem.Domain.Orders;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryManagementSystem.Domain.Customers
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }

        public string  LastName { get; set; }

        public string Location { get; set; }

        public IEnumerable<Order> Orders { get; set; } = [];
    }
}
