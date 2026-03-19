using Microsoft.Extensions.DependencyInjection;
using SportGym.Application.Services.Authentication;

namespace SportGym.Application;

public static class DependencyInjection
{
  public static IServiceCollection AddApplication(this IServiceCollection services)
  {
    services.AddScoped<IAuthenticationService, AuthenticationService>();
    return services;
  }
}