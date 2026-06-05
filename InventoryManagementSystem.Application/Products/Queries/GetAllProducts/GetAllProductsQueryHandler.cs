using InventoryManagementSystem.Application.Shared.Messaging;
using InventoryManagementSystem.Domain.Products;


namespace InventoryManagementSystem.Application.Products.Queries.GetAllProducts
{
    public class GetAllProductsQueryHandler : IQueryHandler<GetAllProductsQuery, IEnumerable<ProductDto>>
    {
        private readonly IProductRepository _productRepository;

        public GetAllProductsQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<IEnumerable<ProductDto>> HandleAsync(GetAllProductsQuery query, CancellationToken cancellationToken = default)
        {
            var products = await _productRepository.GetAllAsync();
            var productDtos = products.Select(p => new ProductDto
            {
                ProductId = p.ProductId,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                Stock = p.Stock
            });
            return productDtos;

        }
    }
}
