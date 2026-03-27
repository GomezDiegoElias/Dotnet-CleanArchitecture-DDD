using ErrorOr;
using MediatR;
using SportGym.Application.Authentication.Common;

namespace SportGym.Application.Authentication.Queries.Login;

public record LoginQuery(
  string Email,
  string Password
) : IRequest<ErrorOr<AuthenticationResult>>;
