using InventoryManagementSystem.Application.Shared.Messaging.Behaviors;
using Microsoft.Extensions.DependencyInjection;

namespace InventoryManagementSystem.Application.Shared.Messaging.Dispatching
{
    public sealed class Dispatcher : IDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public Dispatcher(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

        public Task<TResult> SendAsync<TResult>(ICommand<TResult> command, CancellationToken cancellationToken = default)
        {
            var handlerType = typeof(ICommandHandler<,>).MakeGenericType(command.GetType(), typeof(TResult));
            dynamic handler = _serviceProvider.GetRequiredService(handlerType);
            return handler.HandleAsync((dynamic)command, cancellationToken);
        }

        public Task<TResult> QueryAsync<TResult>(IQuery<TResult> query, CancellationToken cancellationToken = default)
        {
            var handlerType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResult));
            dynamic handler = _serviceProvider.GetRequiredService(handlerType);
            return handler.HandleAsync((dynamic)query, cancellationToken);
        }
    }
}
