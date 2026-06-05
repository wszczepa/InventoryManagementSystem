using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryManagementSystem.Application.Shared.Messaging
{

    public interface IQueryHandler<in TQuery, TResult>
        where TQuery : IQuery<TResult>
    {
        Task<TResult> HandleAsync(TQuery query, CancellationToken cancellationToken = default);
    }
}
