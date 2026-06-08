namespace InventoryManagementSystem.Api.Common
{
    public record ErrorResponse(string Message, string? Details = null);
}
