using InventoryManagementSystem.Api.Contracts.Products;
using InventoryManagementSystem.Application.Products.Command.CreateProduct;
using InventoryManagementSystem.Application.Products.Queries.GetAllProducts;
using InventoryManagementSystem.Application.Shared.Messaging.Dispatching;
using InventoryManagementSystem.Api.Common;
using System.Linq;


namespace InventoryManagementSystem.Api.Endpoints
{
    public static class ProductEndpoints
    {

        public static void MapProductEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("api/products", GetProducts);
            app.MapPost("api/products", CreateProduct);

        }

        public static async Task<IResult> GetProducts(IDispatcher dispatcher)
        {
            try
            {
                var products = await dispatcher.QueryAsync(new GetAllProductsQuery());
                return Results.Ok(products);
            }
            catch (Exception ex)
            {
                return ex.ToErrorResult();
            }
        }

        public static async Task<IResult> CreateProduct(CreateProductRequest request, IDispatcher dispatcher)
        {
            try
            {
                var command = new CreateProductCommand(request.Name, request.Description, request.Price, request.Stock);
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
