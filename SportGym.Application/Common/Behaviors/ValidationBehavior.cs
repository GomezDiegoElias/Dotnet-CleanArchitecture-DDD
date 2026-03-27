using ErrorOr;
using FluentValidation;
using MediatR;
using SportGym.Application.Authentication.Commands.Register;
using SportGym.Application.Authentication.Common;

namespace SportGym.Application.Common.Behaviors;

public class ValidateBehavior<TRequest, TResponse> : 
  IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : IErrorOr
{
  private readonly IValidator<TRequest>? _validator;

  public ValidateBehavior(IValidator<TRequest>? validator = null)
  {
    _validator = validator;
  }

  public async Task<TResponse> Handle(
    TRequest request, 
    RequestHandlerDelegate<TResponse> next, 
    CancellationToken cancellationToken)
  {
    // before the handler
    if (_validator is null)
    {
      return await next();
    }

    var validationResult = await _validator.ValidateAsync(request, cancellationToken);
  
    if (validationResult.IsValid)
    {
      return await next();
    }

    var errors = validationResult.Errors.ConvertAll(e => Error.Validation(e.PropertyName, e.ErrorMessage));

    // after the handler
    return (dynamic)errors;
  }
}