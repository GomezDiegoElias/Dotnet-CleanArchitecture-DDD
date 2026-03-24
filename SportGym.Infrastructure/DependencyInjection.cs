using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SportGym.Application.Common.Interfaces.Authentication;
using SportGym.Application.Common.Interfaces.Persistence;
using SportGym.Application.Common.Interfaces.Services;
using SportGym.Infrastructure.Authentication;
using SportGym.Infrastructure.Persistence;
using SportGym.Infrastructure.Services;

namespace SportGym.Infrastructure;

public static class DependencyInjection
{
  public static IServiceCollection AddInfrastructure(
    this IServiceCollection services,
    ConfigurationManager configuration
  )
  {
    services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
    
    services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
    services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

    services.AddScoped<IUserRepository, UserRepository>();

    return services;
  }
}