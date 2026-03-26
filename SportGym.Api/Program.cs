using Microsoft.AspNetCore.Mvc.Infrastructure;
using SportGym.Api.Common.Errors;
using SportGym.Application;
using SportGym.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddApplication()
        .AddInfrastructure(builder.Configuration);

    builder.Services.AddControllers();
    builder.Services.AddSingleton<ProblemDetailsFactory, SportGymProblemDetailsFactory>();

    builder.Services.AddOpenApi();
}

var app = builder.Build();
{
    if (app.Environment.IsDevelopment())
    {
        app.MapOpenApi();
    }

    app.UseExceptionHandler("/error");

    app.UseHttpsRedirection();
    app.MapControllers();

    app.Run();
}