using FluentValidation;
using MediatR;

namespace BudgetPlaner.Application.Common.Behaviours
{
    internal class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            // 1. Gibt es überhaupt Regeln für diesen Request? Wenn nein, weitergehen.
            if (!_validators.Any())
            {
                return await next();
            }

            // 2. Kontext erstellen
            var context = new ValidationContext<TRequest>(request);

            // 3. Alle Validatoren ausführen (parallel)
            var validationResults = await Task.WhenAll(
                _validators.Select(v => v.ValidateAsync(context, cancellationToken)));

            // 4. Fehler sammeln
            var failures = validationResults
                .SelectMany(r => r.Errors)
                .Where(f => f != null)
                .ToList();

            // 5. Wenn Fehler da sind -> BÄM! Exception werfen. Die Pipeline bricht hier ab.
            if (failures.Count != 0)
            {
                throw new ValidationException(failures);
            }

            // 6. Wenn alles gut ist -> Weiter zum nächsten Schritt (dem Handler)
            return await next();
        }
    }
}
