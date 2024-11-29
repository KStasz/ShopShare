using FluentValidation;
using MediatR;
using ShopShare.Domain.Common.Models;

namespace ShopShare.Application.Common.Validation
{
    public class ValidationBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : Result
    {
        private readonly IValidator<TRequest>? _validator;

        public ValidationBehavior(IValidator<TRequest>? validator)
        {
            _validator = validator;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            if (_validator is null)
            {
                return await next();
            }

            var validatorResult = await _validator.ValidateAsync(
                request,
                cancellationToken);

            if (!validatorResult.IsValid)
            {
                var errors = validatorResult.Errors;

                return (dynamic)Result.Failure(new Error(
                    errors.Select(x => $"Validation.{x.PropertyName}"),
                    errors.Select(x => x.ErrorMessage)));
            }

            return await next();
        }
    }
}
