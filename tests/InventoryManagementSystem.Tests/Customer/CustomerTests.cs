using InventoryManagementSystem.Domain.Customers;
using NUnit.Framework;
using System;

namespace InventoryManagementSystem.Tests.Customer
{
    [TestFixture]
    public class CustomerTests
    {
        [Test]
        public void Ctor_RequiresNameAndLastName()
        {
            Assert.Throws<ArgumentException>(() => new Domain.Customers.Customer("", "Last", CustomerRegion.Europe));
            Assert.Throws<ArgumentException>(() => new Domain.Customers.Customer("Name", "", CustomerRegion.Europe));
        }
    }
}
