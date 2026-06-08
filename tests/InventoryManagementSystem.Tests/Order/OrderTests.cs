using InventoryManagementSystem.Domain.Orders;
using NUnit.Framework;
using System;

namespace InventoryManagementSystem.Tests.Order
{
    [TestFixture]
    public class OrderTests
    {
        [Test]
        public void Ctor_RequiresCustomerId()
        {
            Assert.Throws<ArgumentException>(() => new Domain.Orders.Order(0));
        }

        [Test]
        public void AddItem_ValidatesInputs()
        {
            var order = new Domain.Orders.Order(1);
            Assert.Throws<ArgumentException>(() => order.AddItem(0, 1, 1m, 1m));
            Assert.Throws<ArgumentException>(() => order.AddItem(1, 0, 1m, 1m));
            Assert.Throws<ArgumentException>(() => order.AddItem(1, 1, -1m, 1m));
        }

        [Test]
        public void ApplyPricing_ComputesTotals()
        {
            var order = new Domain.Orders.Order(1);
            order.AddItem(1, 2, 10m, 10m);
            order.AddItem(2, 1, 5m, 5m);

            order.ApplyPricing(3m);
            Assert.AreEqual(25m, order.Subtotal);
            Assert.AreEqual(3m, order.Discount);
            Assert.AreEqual(22m, order.Total);
        }
    }
}
