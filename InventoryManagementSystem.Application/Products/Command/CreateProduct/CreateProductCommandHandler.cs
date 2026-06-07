using InventoryManagementSystem.Application.Products.Command.CreateProduct;
using InventoryManagementSystem.Application.Shared.Messaging;
using InventoryManagementSystem.Domain.Products;
using InventoryManagementSystem.Infrastructure.Persistence;

namespace InventoryManagementSystem.Application.Products.Command.AddProduct
{
    public class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, int>
    {
        private readonly IProductRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateProductCommandHandler(IProductRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }
        public async Task<int> HandleAsync(CreateProductCommand command, CancellationToken cancellationToken = default)
        {
            var product = new Product
            {
                Name = command.Name,
                Description = command.Description,
                Price = command.Price,
                Stock = command.Stock
            };

            await _repository.CreateAsync(product);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return product.ProductId;
        }
    }
}
