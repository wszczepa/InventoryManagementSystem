using InventoryManagementSystem.Application.Shared.Messaging;
using InventoryManagementSystem.Domain.Common;
using InventoryManagementSystem.Domain.Customers;
using InventoryManagementSystem.Domain.Orders;
using InventoryManagementSystem.Domain.Pricing;
using InventoryManagementSystem.Domain.Pricing.Rules.PriceModeifierRules;
using InventoryManagementSystem.Domain.Products;
using InventoryManagementSystem.Infrastructure.Persistence;

namespace InventoryManagementSystem.Application.Orders.Command
{
    public class CreateOrderCommandHandler : ICommandHandler<CreateOrderCommand, int>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductRepository _productRepository;
        private readonly IPricingService _pricingService;
        private readonly IDateTimeProvider _clock;
        private readonly ICustomerRepository _customerRepository;
        private readonly ILocationPricingRule _locationPricingRule;

        public CreateOrderCommandHandler(IUnitOfWork unitOfWork, IOrderRepository orderRepository, IProductRepository productRepository, IPricingService pricingService, IDateTimeProvider clock, ICustomerRepository customerRepository, ILocationPricingRule locationPricingRule)
        {
            _unitOfWork = unitOfWork;
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _pricingService = pricingService;
            _clock = clock;
            _customerRepository = customerRepository;
            _locationPricingRule = locationPricingRule;
        }
        public async Task<int> HandleAsync(CreateOrderCommand command, CancellationToken cancellationToken = default)
        {
            var customer = await _customerRepository.GetByIdAsync(command.CustomerId) ?? throw new KeyNotFoundException("Customer not found");

            var products = (await _productRepository.GetAsync(command.Items.Select(x => x.ProductId))).ToDictionary(x => x.ProductId);

            var order = new Order(command.CustomerId);
           
            foreach (var item in command.Items)
            {
                if (!products.TryGetValue(item.ProductId, out var product))
                    throw new KeyNotFoundException($"Product {item.ProductId} does not exist");

                product.Reserve(item.Quantity);


                order.AddItem(product.ProductId, item.Quantity, product.Price, _locationPricingRule.Apply(customer.Region, product.Price));
            }

            var discount = _pricingService.CalculateBestDiscount(new PricingContext
            {
                Now = _clock.UtcNow,
                Order = order,
                Customer = customer!
            });

            order.ApplyPricing(discount);

            await _orderRepository.CreateAsync(order);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return order.OrderId;

        }
 
    }
}
