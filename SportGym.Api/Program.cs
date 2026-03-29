using SportGym.Api;
using SportGym.Application;
using SportGym.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddPresentation()
        .AddApplication()
        .AddInfrastructure(builder.Configuration);
}

var app = builder.Build();
{
    if (app.Environment.IsDevelopment())
    {
        app.MapOpenApi();
    }

    //app.UseExceptionHandler("/error");
    app.UseExceptionHandler(); // Use the default exception handler which will invoke our IExceptionHandler implementations

    app.UseHttpsRedirection();
    app.MapControllers();

    app.Run();
}
