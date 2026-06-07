using Azure.Core;
using InventoryManagementSystem.Application.Shared.Messaging;
using InventoryManagementSystem.Domain.Exceptions;
using InventoryManagementSystem.Domain.Orders;
using InventoryManagementSystem.Domain.Products;
using InventoryManagementSystem.Infrastructure.Persistence;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.Application.Orders.Command
{
    public class CreateOrderCommandHandler : ICommandHandler<CreateOrderCommand, int>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductRepository _productRepository;

        public CreateOrderCommandHandler(IUnitOfWork unitOfWork, IOrderRepository orderRepository, IProductRepository productRepository)
        {
            _unitOfWork = unitOfWork;
            _orderRepository = orderRepository;
            _productRepository = productRepository;
        }
        public async Task<int> HandleAsync(CreateOrderCommand command, CancellationToken cancellationToken = default)
        {
            var products = await _productRepository.GetAsync(command.Items.Select(x => x.ProductId));

            var order = new Order(command.CustomerId);
           
            foreach (var item in command.Items)
            {
                var product = products.FirstOrDefault(x => x.ProductId == item.ProductId)
                    ?? throw new DomainException(
                        $"Product {item.ProductId} does not exist");

                product.Reserve(item.Quantity);

                order.AddItem(product.ProductId, item.Quantity);
            }

            await _orderRepository.CreateAsync(order);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return order.OrderId;

        }
 
    }
}
