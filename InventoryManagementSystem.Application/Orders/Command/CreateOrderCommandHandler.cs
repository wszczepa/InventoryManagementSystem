using InventoryManagementSystem.Application.Shared.Messaging;
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

            ValidateStock(command.Items, products.ToList());

            var order = new Order
            {
                CustomerId = command.CustomerId,
                Items = command.Items.Select(i => new Domain.Orders.OrderItem
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity
                }).ToList()
            };

            await _orderRepository.CreateAsync(order);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return order.OrderId;

        }


        private static void ValidateStock(
            IEnumerable<OrderItem> items,
            List<Product> products)
        {
            var failures = new List<string>();

            foreach (var item in items)
            {
                var product = products.FirstOrDefault(x => x.ProductId == item.ProductId);

                if (product is null)
                {
                    failures.Add($"Product {item.ProductId} does not exist");
                    continue;
                }

                if (product.Stock < item.Quantity)
                {
                    failures.Add($"Not enough stock for product {product.ProductId}");
                }
            }

            if (failures.Any())
                throw new ValidationException(string.Join(", ", failures));
        }
 
    }
}
