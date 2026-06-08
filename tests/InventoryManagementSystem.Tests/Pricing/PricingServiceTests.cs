using InventoryManagementSystem.Domain.Pricing;
using InventoryManagementSystem.Domain.Pricing.Rules.DiscountRules;
using InventoryManagementSystem.Domain.Orders;
using NUnit.Framework;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace InventoryManagementSystem.Tests.Pricing
{
    [TestFixture]
    public class PricingServiceTests
    {
        [Test]
        public void CalculateBestDiscount_ReturnsMaxOfRules()
        {
            var rule1 = new Mock<IDiscountRule>();
            var rule2 = new Mock<IDiscountRule>();
            var rule3 = new Mock<IDiscountRule>();

            rule1.Setup(r => r.Calculate(It.IsAny<PricingContext>(), It.IsAny<decimal>())).Returns(1m);
            rule2.Setup(r => r.Calculate(It.IsAny<PricingContext>(), It.IsAny<decimal>())).Returns(5m);
            rule3.Setup(r => r.Calculate(It.IsAny<PricingContext>(), It.IsAny<decimal>())).Returns(3m);

            var svc = new PricingService(new IDiscountRule[] { rule1.Object, rule2.Object, rule3.Object });

            var ctx = new PricingContext { Now = DateTime.UtcNow, Order = new Domain.Orders.Order(1) };

            var result = svc.CalculateBestDiscount(ctx);

            Assert.AreEqual(5m, result);
        }
    }
}
