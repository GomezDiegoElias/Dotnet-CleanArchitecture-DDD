using Microsoft.Extensions.DependencyInjection;
using SportGym.Application.Services.Authentication.Commands;
using SportGym.Application.Services.Authentication.Queries;

namespace SportGym.Application;

public static class DependencyInjection
{
  public static IServiceCollection AddApplication(this IServiceCollection services)
  {
    services.AddScoped<IAuthenticationCommandService, AuthenticationCommandService>();
    services.AddScoped<IAuthenticationQueryService, AuthenticationQueryService>();
    return services;
  }
}