using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryManagementSystem.Application.Shared.Messaging.Behaviors
{
    public delegate Task<TResponse> RequestHandlerDelegate<TResponse>(CancellationToken cancellationToken = default);

    public interface IPipelineBehavior<TRequest, TResponse>
    {
        Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken);
    }
}
