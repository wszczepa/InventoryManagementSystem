namespace InventoryManagementSystem.Domain.Common
{
    public interface IDateTimeProvider
    {
        DateTime UtcNow { get; }
    }
}
