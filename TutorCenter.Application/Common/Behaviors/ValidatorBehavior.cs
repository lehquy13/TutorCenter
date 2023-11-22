using FluentValidation;
using MediatR;

namespace TutorCenter.Application.Common.Behaviors;

public class ValidationBehavior<TRequest, TResponse> :
    IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>

{
    private readonly IValidator<TRequest>? _validator;

    public ValidationBehavior(IValidator<TRequest>? validator = null)
    {
        _validator = validator;
    }

    public async Task<TResponse> Handle(TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (_validator == null) return await next();
        // before the handler
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        // after the handler
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.ToList();
            throw new Exception(errors.FirstOrDefault()?.ErrorMessage);
        }

        return await next();
    }
}