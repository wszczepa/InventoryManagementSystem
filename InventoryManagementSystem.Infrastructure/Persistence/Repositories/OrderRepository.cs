using InventoryManagementSystem.Domain.Orders;
namespace InventoryManagementSystem.Infrastructure.Persistence.Repositories
{
    internal class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;
        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task CreateAsync(Order order)
        {
            await _context.AddAsync(order);
        }
    }
}
