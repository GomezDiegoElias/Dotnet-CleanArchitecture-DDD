using ErrorOr;
using SportGym.Application.Services.Authentication.Common;

namespace SportGym.Application.Services.Authentication.Queries;

public interface IAuthenticationQueryService
{
  ErrorOr<AuthenticationResult> Login(string email, string password);
}