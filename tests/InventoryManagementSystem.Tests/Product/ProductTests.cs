using InventoryManagementSystem.Domain.Products;
using NUnit.Framework;
using System;

namespace InventoryManagementSystem.Tests.Product
{
    public class ProductTests
    {
        [Test]
        public void Reserve_DecreasesStock_ThrowsWhenInsufficient()
        {
            var product = new Domain.Products.Product("Name", "Desc", 10m, 5);

            product.Reserve(2);

            Assert.AreEqual(3, product.Stock);

            var ex = Assert.Throws<ArgumentException>(() => product.Reserve(10));
            StringAssert.Contains("Not enough stock", ex.Message);
        }

        [Test]
        public void Restock_IncreasesStock_ThrowsOnInvalid()
        {
            var product = new Domain.Products.Product("Name", "Desc", 10m, 5);

            product.Restock(3);

            Assert.AreEqual(8, product.Stock);

            var ex = Assert.Throws<ArgumentException>(() => product.Restock(0));
            StringAssert.Contains("greater than zero", ex.Message);
        }
    }
}
