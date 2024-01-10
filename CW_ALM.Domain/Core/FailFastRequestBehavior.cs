using CW_ALM.Domain.Commands;
using CW_ALM.Resources;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.Localization;

namespace CW_ALM.Domain.Core
{
    public class FailFastRequestBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse> where TResponse : CommandResult
    {
        private readonly IEnumerable<IValidator> _validators;
        IStringLocalizer<SharedResource_Domain> _localizer;

        public FailFastRequestBehavior(IEnumerable<IValidator<TRequest>> validators, IStringLocalizer<SharedResource_Domain> localizer)
        {
            _validators = validators;
            _localizer = localizer;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            
            var context = new ValidationContext<TRequest>(request);

            var validationFailures = await Task.WhenAll(_validators.Select(validator => validator.ValidateAsync(context)));

            var failures = _validators
                .Select(v => v.Validate(context))
                .SelectMany(result => result.Errors)
                .Where(f => f != null)
                .ToList();

            return await (failures.Any()
                ? Errors(failures)
                : next()).ConfigureAwait(false);
        }

        private Task<TResponse> Errors(IEnumerable<ValidationFailure> failures)
        {
            CommandResult response = new(
                success: false,
                message: _localizer["Resx_Domain_ErroValidacaoRequisicao"],
                data: null);

            foreach (var failure in failures.DistinctBy(a => a.PropertyName))
            {
                response.AddError(failure.PropertyName, _localizer[failure.ErrorMessage].Value.ToString());
            }

            return Task.FromResult(response as TResponse)!;
        }
    }
}
