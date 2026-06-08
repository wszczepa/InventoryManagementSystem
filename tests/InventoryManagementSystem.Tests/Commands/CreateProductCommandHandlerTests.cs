using InventoryManagementSystem.Application.Products.Command.AddProduct;
using InventoryManagementSystem.Application.Products.Command.CreateProduct;
using InventoryManagementSystem.Domain.Products;
using InventoryManagementSystem.Infrastructure.Persistence;
using Moq;
using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Tests.Commands
{
    [TestFixture]
    public class CreateProductCommandHandlerTests
    {
        private Mock<IProductRepository> _repoMock = null!;
        private Mock<IUnitOfWork> _uowMock = null!;
        private CreateProductCommandHandler? _handler;

        [SetUp]
        public void SetUp()
        {
            _repoMock = new Mock<IProductRepository>();
            _uowMock = new Mock<IUnitOfWork>();
            _repoMock.Setup(r => r.CreateAsync(It.IsAny<Domain.Products.Product>())).Returns(Task.CompletedTask);
            _uowMock.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(0);

            _handler = new CreateProductCommandHandler(_repoMock.Object, _uowMock.Object);
        }

        [Test]
        public async Task HandleAsync_CreatesProduct_AndSaves()
        {
            var cmd = new CreateProductCommand("p1","desc", 9.99m, 5);

            var id = await _handler!.HandleAsync(cmd);

            _repoMock.Verify(r => r.CreateAsync(It.IsAny<Domain.Products.Product>()), Times.Once);
            _uowMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
            Assert.GreaterOrEqual(id, 0);
        }
    }
}
