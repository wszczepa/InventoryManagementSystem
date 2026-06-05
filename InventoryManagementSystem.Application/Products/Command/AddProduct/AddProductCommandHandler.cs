using InventoryManagementSystem.Application.Shared.Messaging;
using InventoryManagementSystem.Domain.Products;
using InventoryManagementSystem.Infrastructure.Persistence;

namespace InventoryManagementSystem.Application.Products.Command.AddProduct
{
    public class AddProductCommandHandler : ICommandHandler<AddProductCommand, int>
    {
        private readonly IProductRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public AddProductCommandHandler(IProductRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }
        public async Task<int> HandleAsync(AddProductCommand command, CancellationToken cancellationToken = default)
        {
            var product = new Product
            {
                Name = command.Name,
                Description = command.Description,
                Price = command.Price,
                Stock = command.Stock
            };

            await _repository.AddAsync(product);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return product.ProductId;
        }
    }
}
