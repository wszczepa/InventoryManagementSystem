using InventoryManagementSystem.Domain.Common;
using InventoryManagementSystem.Domain.Pricing;
using InventoryManagementSystem.Domain.Pricing.Rules.DiscountRules;
using InventoryManagementSystem.Domain.Pricing.Rules.DiscountRules.HolidayDiscountRule;
using InventoryManagementSystem.Domain.Pricing.Rules.DiscountRules.HolidayDiscountRule.HolidayCalendar;
using InventoryManagementSystem.Domain.Pricing.Rules.PriceModeifierRules;
using Microsoft.Extensions.DependencyInjection;

namespace InventoryManagementSystem.Domain
{
    public static class DomainServiceRegistration
    {
        public static IServiceCollection AddDomain(
        this IServiceCollection services)
        {

            services.AddScoped<IPricingService, PricingService>();
            services.AddScoped<ILocationPricingRule, LocationPricingRule>();
            services.AddScoped<IHolidayCalendar, HolidayCalendar>();
            services.AddScoped<IDiscountRule, HolidayDiscountRule>();
            services.AddScoped<IDiscountRule, BlackFridayDiscountRule>();
            services.AddScoped<IDiscountRule, VolumeDiscountRule>();
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();


            return services;
        }
    }
}
