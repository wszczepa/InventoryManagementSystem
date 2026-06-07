namespace InventoryManagementSystem.Domain.Orders
{
    public interface IOrderRepository
    {
        Task CreateAsync(Order Order);
    }
}
