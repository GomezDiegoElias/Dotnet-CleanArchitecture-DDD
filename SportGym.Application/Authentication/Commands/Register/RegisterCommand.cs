using ErrorOr;
using MediatR;
using SportGym.Application.Authentication.Common;

namespace SportGym.Application.Authentication.Commands.Register;

public record RegisterCommand(
  string FirstName,
  string LastName,
  string Email,
  string Password
) : IRequest<ErrorOr<AuthenticationResult>>;
