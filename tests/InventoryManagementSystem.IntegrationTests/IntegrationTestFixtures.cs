using InventoryManagementSystem.Api;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace InventoryManagementSystem.IntegrationTests
{
    public class CustomFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(d => d.ServiceType.Name.Contains("ApplicationDbContext"));
                if (descriptor != null)
                    services.Remove(descriptor);

                services.AddDbContext<InventoryManagementSystem.Infrastructure.Persistence.ApplicationDbContext>(options =>
                    options.UseInMemoryDatabase("TestDb"));
            });
        }
    }
}
