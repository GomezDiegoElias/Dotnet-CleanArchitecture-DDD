using Microsoft.AspNetCore.Mvc.Infrastructure;
using SportGym.Api.Common.Errors;
using SportGym.Api.Common.Mapping;

namespace SportGym.Api;

public static class DependencyInjection
{
  public static IServiceCollection AddPresentation(this IServiceCollection services)
  {
    services.AddControllers();
    services.AddSingleton<ProblemDetailsFactory, SportGymProblemDetailsFactory>();
    services.AddOpenApi();
    
    services.AddMappings();
    return services;
  }
}