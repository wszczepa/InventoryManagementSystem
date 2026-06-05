using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryManagementSystem.Application.Shared.Messaging.Dispatching
{
    public interface IDispatcher
    {
        Task<TResult> SendAsync<TResult>(ICommand<TResult> command, CancellationToken cancellationToken = default);
        Task<TResult> QueryAsync<TResult>(IQuery<TResult> query, CancellationToken cancellationToken = default);
    }
}
