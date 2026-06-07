using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryManagementSystem.Domain.Pricing.Rules.DiscountRules.HolidayDiscountRule.HolidayCalendar
{
    public interface IHolidayCalendar
    {
        bool IsHoliday(DateTime date);
    }
}
