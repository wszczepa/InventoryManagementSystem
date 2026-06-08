using InventoryManagementSystem.Domain.Pricing.Rules.DiscountRules;
using InventoryManagementSystem.Domain.Pricing.Rules.DiscountRules.HolidayDiscountRule;
using InventoryManagementSystem.Domain.Pricing.Rules.DiscountRules.HolidayDiscountRule.HolidayCalendar;
using InventoryManagementSystem.Domain.Common;
using NUnit.Framework;
using Moq;
using System;
using InventoryManagementSystem.Domain.Pricing;

namespace InventoryManagementSystem.Tests.Pricing
{
    [TestFixture]
    public class DiscountRulesTests
    {
        private Mock<IDateTimeProvider>? _clockMock;
        private Mock<IHolidayCalendar>? _calendarMock;

        [SetUp]
        public void SetUp()
        {
            _clockMock = new Mock<IDateTimeProvider>();
            _calendarMock = new Mock<IHolidayCalendar>();

            _clockMock.Setup(c => c.UtcNow).Returns(DateTime.UtcNow);
            _calendarMock.Setup(c => c.IsHoliday(It.IsAny<DateTime>())).Returns(false);
        }

        [Test]
        public void HolidayDiscount_ReturnsZero_WhenNotHoliday()
        {
            var rule = new HolidayDiscountRule(_clockMock!.Object, _calendarMock!.Object);
            var ctx = new PricingContext { Now = _clockMock.Object.UtcNow, Order = new Domain.Orders.Order(1) };

            var result = rule.Calculate(ctx, 100m);

            Assert.AreEqual(0m, result);
        }

        [Test]
        public void HolidayDiscount_Returns15PercentOfMostExpensiveItem_WhenHoliday()
        {
            var now = new DateTime(2025, 12, 25);
            _clockMock!.Setup(c => c.UtcNow).Returns(now);
            _calendarMock!.Setup(c => c.IsHoliday(now)).Returns(true);

            var order = new Domain.Orders.Order(1);
            order.AddItem(1, 2, 10m, 10m); 
            order.AddItem(2, 1, 50m, 50m); 

            var rule = new HolidayDiscountRule(_clockMock.Object, _calendarMock.Object);
            var ctx = new PricingContext { Now = now, Order = order };

            var result = rule.Calculate(ctx, 100m);

            Assert.AreEqual(50m * 0.15m, result);
        }

        [Test]
        public void BlackFriday_Returns25Percent_WhenDateInRange()
        {
            var rule = new BlackFridayDiscountRule();
            var ctx = new PricingContext { Now = new DateTime(2025, 11, 25), Order = new Domain.Orders.Order(1) };

            var result = rule.Calculate(ctx, 200m);

            Assert.AreEqual(200m * 0.25m, result);
        }

        [Test]
        public void BlackFriday_ReturnsZero_WhenOutsideRange()
        {
            var rule = new BlackFridayDiscountRule();
            var ctx = new PricingContext { Now = new DateTime(2025, 10, 1), Order = new Domain.Orders.Order(1) };

            var result = rule.Calculate(ctx, 200m);

            Assert.AreEqual(0m, result);
        }

        [Test]
        public void VolumeDiscount_AppliesCorrectly()
        {
            var rule = new VolumeDiscountRule();
            var order = new Domain.Orders.Order(1);
            order.AddItem(1, 2, 10m, 10m); 
            var ctx = new PricingContext { Now = DateTime.UtcNow, Order = order };
            Assert.AreEqual(0m, rule.Calculate(ctx, 20m));

            order.AddItem(2, 3, 5m, 5m); 
            Assert.AreEqual(20m * 0.10m, rule.Calculate(ctx, 20m));


            order.AddItem(3, 5, 1m, 1m); 
            Assert.AreEqual(20m * 0.20m, rule.Calculate(ctx, 20m));

            order.AddItem(4, 40, 1m, 1m); 
            Assert.AreEqual(20m * 0.30m, rule.Calculate(ctx, 20m));
        }
    }
}
