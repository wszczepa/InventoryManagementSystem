using InventoryManagementSystem.Api.Contracts.Orders;
using InventoryManagementSystem.Application.Orders.Command;
using InventoryManagementSystem.Application.Shared.Messaging.Dispatching;
using InventoryManagementSystem.Api.Common;
using System.Linq;

namespace InventoryManagementSystem.Api.Endpoints
{
    public static class OrderEndpoints
    {

        public static void MapOrderEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("api/orders", CreateOrder);

        }

        public static async Task<IResult> CreateOrder(CreateOrderRequest request, IDispatcher dispatcher)
        { 
            try
            {
                var command = new CreateOrderCommand(request.CustomerId, request.Items.Select(i => new OrderItem(i.ProductId, i.Quantity)));
                var id = await dispatcher.SendAsync(command);
                return Results.Ok(id);
            }
            catch (Exception ex)
            {
                return ex.ToErrorResult();
            }
        }
    }
}
