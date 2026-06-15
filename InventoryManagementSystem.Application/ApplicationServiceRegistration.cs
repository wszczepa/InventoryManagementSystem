using FluentValidation;
using InventoryManagementSystem.Application.Shared.Messaging;
using InventoryManagementSystem.Application.Shared.Messaging.Dispatching;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace InventoryManagementSystem.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {

            var assembly = typeof(ApplicationAssemblyMarker).Assembly;

            RegisterHandlers(services, assembly, typeof(ICommandHandler<,>));
            RegisterHandlers(services, assembly, typeof(IQueryHandler<,>));


            services.AddValidatorsFromAssembly(assembly);

            // Inner dispatcher resolved by type (internal), outer wraps it with validation
            services.AddScoped<Dispatcher>();
            services.AddScoped<IDispatcher>(sp =>
                new ValidatingDispatcher(sp.GetRequiredService<Dispatcher>(), sp));

            return services;
        }


        private static void RegisterHandlers(IServiceCollection services, Assembly assembly, Type handlerInterfaceType)
        {
            var registrations = assembly.GetTypes()
                .Where(t => t is { IsClass: true, IsAbstract: false })
                .SelectMany(t => t.GetInterfaces()
                    .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == handlerInterfaceType)
                    .Select(i => new { Implementation = t, Interface = i }));

            foreach (var reg in registrations)
                services.AddScoped(reg.Interface, reg.Implementation);
        }
    }
}
