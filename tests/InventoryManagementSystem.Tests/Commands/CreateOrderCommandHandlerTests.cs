using InventoryManagementSystem.Application.Orders.Command;
using InventoryManagementSystem.Domain.Orders;
using InventoryManagementSystem.Domain.Products;
using InventoryManagementSystem.Domain.Customers;
using InventoryManagementSystem.Infrastructure.Persistence;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace InventoryManagementSystem.Tests.Commands
{
    [TestFixture]
    public class CreateOrderCommandHandlerTests
    {
        private Mock<IOrderRepository> _orderRepo = null!;
        private Mock<IProductRepository> _productRepo = null!;
        private Mock<ICustomerRepository> _customerRepo = null!;
        private Mock<IUnitOfWork> _uow = null!;
        private Mock<InventoryManagementSystem.Domain.Pricing.IPricingService> _pricing = null!;
        private Mock<InventoryManagementSystem.Domain.Common.IDateTimeProvider> _clock = null!;
        private Mock<InventoryManagementSystem.Domain.Pricing.Rules.PriceModeifierRules.ILocationPricingRule> _locationRule = null!;
        private CreateOrderCommandHandler? _handler;

        [SetUp]
        public void SetUp()
        {
            _orderRepo = new Mock<IOrderRepository>();
            _productRepo = new Mock<IProductRepository>();
            _customerRepo = new Mock<ICustomerRepository>();
            _uow = new Mock<IUnitOfWork>();
            _pricing = new Mock<InventoryManagementSystem.Domain.Pricing.IPricingService>();
            _clock = new Mock<InventoryManagementSystem.Domain.Common.IDateTimeProvider>();
            _locationRule = new Mock<InventoryManagementSystem.Domain.Pricing.Rules.PriceModeifierRules.ILocationPricingRule>();

            _orderRepo.Setup(o => o.CreateAsync(It.IsAny<Domain.Orders.Order>())).Returns(Task.CompletedTask);
            _uow.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(0);
            _pricing.Setup(p => p.CalculateBestDiscount(It.IsAny<InventoryManagementSystem.Domain.Pricing.PricingContext>())).Returns(0m);
            _locationRule.Setup(l => l.Apply(It.IsAny<CustomerRegion>(), It.IsAny<decimal>())).Returns((CustomerRegion r, decimal price) => price);

            _handler = new CreateOrderCommandHandler(_uow.Object, _orderRepo.Object, _productRepo.Object, _pricing.Object, _clock.Object, _customerRepo.Object, _locationRule.Object);
        }

        [Test]
        public async Task HandleAsync_CreatesOrder_WhenEverythingExists()
        {
            var product = new Domain.Products.Product("n", "d", 10m, 10);

            var pidProp = typeof(Domain.Products.Product).GetProperty("ProductId", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public);
            pidProp!.SetValue(product, 1);
            _productRepo.Setup(p => p.GetAsync(It.IsAny<IEnumerable<int>>())).ReturnsAsync(new[] { product });
            _customerRepo.Setup(c => c.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(new Domain.Customers.Customer("a", "b", InventoryManagementSystem.Domain.Customers.CustomerRegion.Europe));

            var cmd = new CreateOrderCommand(1, new[] { new InventoryManagementSystem.Application.Orders.Command.OrderItem(1, 2) });

            var id = await _handler!.HandleAsync(cmd);

            _orderRepo.Verify(r => r.CreateAsync(It.IsAny<Domain.Orders.Order>()), Times.Once);
            _uow.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
            Assert.GreaterOrEqual(id, 0);
        }

        [Test]
        public void HandleAsync_ThrowsWhenCustomerMissing()
        {
            _customerRepo.Setup(c => c.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Domain.Customers.Customer?)null);

            var cmd = new CreateOrderCommand(1, new[] { new InventoryManagementSystem.Application.Orders.Command.OrderItem(1, 2) });

            Assert.ThrowsAsync<KeyNotFoundException>(async () => await _handler!.HandleAsync(cmd));
        }
    }
}
