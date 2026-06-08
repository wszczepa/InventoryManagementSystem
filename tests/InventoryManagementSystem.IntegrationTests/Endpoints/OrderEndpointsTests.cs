using System.Net.Http.Json;
using InventoryManagementSystem.Api.Contracts.Orders;
using NUnit.Framework;
using System.Threading.Tasks;

namespace InventoryManagementSystem.IntegrationTests.Endpoints
{
    [TestFixture]
    public class OrderEndpointsTests
    {
        private CustomFactory? _factory;

        [SetUp]
        public void SetUp()
        {
            _factory = new CustomFactory();
        }

        [TearDown]
        public async Task TearDown()
        {
            if (_factory != null)
                await _factory.DisposeAsync();
        }

        [Test]
        public async Task CreateOrder_ReturnsBadRequest_ForNullRequest()
        {
            var client = _factory!.CreateClient();

            var response = await client.PostAsJsonAsync("/api/orders", (CreateOrderRequest?)null);

            Assert.AreEqual(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
