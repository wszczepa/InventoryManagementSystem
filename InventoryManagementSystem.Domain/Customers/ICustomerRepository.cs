using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryManagementSystem.Domain.Customers
{
    public interface ICustomerRepository
    {
        Task<Customer?> GetByIdAsync(int id);
    }
}
