using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation.Results;

namespace InventoryManagementSystem.Application.Shared.Messaging.Dispatching
{
    public class ValidatingDispatcher : IDispatcher
    {

        private readonly IDispatcher _inner;
        private readonly IServiceProvider _serviceProvider;

        public ValidatingDispatcher(IDispatcher inner, IServiceProvider serviceProvider)
        {
            _inner = inner;
            _serviceProvider = serviceProvider;
        }

        public async Task<TResult> SendAsync<TResult>(ICommand<TResult> command, CancellationToken cancellationToken = default)
        {
            var commandType = command.GetType();
            var validatorInterfaceType = typeof(IValidator<>).MakeGenericType(commandType);
            var validators = _serviceProvider.GetServices(validatorInterfaceType).ToList();

            if (validators.Count > 0)
            {
                var failures = new List<ValidationFailure>();

                foreach (dynamic validator in validators)
                {
                    ValidationResult result = await validator.ValidateAsync((dynamic)command, cancellationToken);
                    failures.AddRange(result.Errors);
                }

                if (failures.Count > 0)
                    throw new ValidationException(failures);
            }

            return await _inner.SendAsync(command, cancellationToken);
        }

        public Task<TResult> QueryAsync<TResult>(IQuery<TResult> query, CancellationToken cancellationToken = default)
            => _inner.QueryAsync(query, cancellationToken);
    }
}
