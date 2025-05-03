
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

public static class MigrationExtensions
{
    public static void ApplyMigrations<TContext>(this IApplicationBuilder app) where TContext : DbContext
    {
        using var scope = app.ApplicationServices.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<TContext>();

        try
        {
            db.Database.Migrate();
        }
        catch (Exception ex)
        {
           
            Console.WriteLine($"Error applying migrations: {ex.Message}");
            throw;
        }
    }
}