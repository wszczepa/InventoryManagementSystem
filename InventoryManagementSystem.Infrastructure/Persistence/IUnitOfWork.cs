using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryManagementSystem.Infrastructure.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
