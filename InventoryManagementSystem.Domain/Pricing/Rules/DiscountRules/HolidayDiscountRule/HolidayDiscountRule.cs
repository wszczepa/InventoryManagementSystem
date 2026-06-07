using InventoryManagementSystem.Domain.Common;
using InventoryManagementSystem.Domain.Pricing.Rules.DiscountRules.HolidayDiscountRule.HolidayCalendar;

namespace InventoryManagementSystem.Domain.Pricing.Rules.DiscountRules.HolidayDiscountRule
{
    internal class HolidayDiscountRule : IDiscountRule
    {
        private readonly IDateTimeProvider _clock;
        private readonly IHolidayCalendar _holidayCalendar;

        public HolidayDiscountRule(IDateTimeProvider clock, IHolidayCalendar holidayCalendar)
        {
            _clock = clock;
            _holidayCalendar = holidayCalendar;
        }

        public decimal Calculate(PricingContext context, decimal subtotal)
        {
            if (!_holidayCalendar.IsHoliday(_clock.UtcNow))
                return 0;

            var mostExpensive = context.Order.Items
                .OrderByDescending(x => x.FinalUnitPrice * x.Quantity)
                .FirstOrDefault();

            if (mostExpensive is null)
                return 0;

            return mostExpensive.FinalUnitPrice * mostExpensive.Quantity * 0.15m;
        }
    }
}
