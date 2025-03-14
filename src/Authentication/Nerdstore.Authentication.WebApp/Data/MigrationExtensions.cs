using Microsoft.EntityFrameworkCore;

namespace Nerdstore.Authentication.WebApp.Data;

public static class MigrationExtensions
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();
        using ApplicationDbContext context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        if (!context.Database.CanConnect())
        {
            context.Database.EnsureCreated();
            context.Database.Migrate();
        }
    }
}