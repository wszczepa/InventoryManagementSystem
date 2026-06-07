using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryManagementSystem.Domain.Common
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;

    }
}
