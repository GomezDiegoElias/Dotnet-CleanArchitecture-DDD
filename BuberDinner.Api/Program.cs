using BuberDinner.Api;
using BuberDinner.Application;
using BuberDinner.Infrastructure;
using BuberDinner.Infrastructure.Persistence;

using Microsoft.EntityFrameworkCore;

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

    app.UseAuthentication();
    app.UseAuthorization();

    app.UseHttpsRedirection();
    app.MapControllers();

    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<BuberDinnerDbContext>();
    await db.Database.MigrateAsync();

    app.Run();
}