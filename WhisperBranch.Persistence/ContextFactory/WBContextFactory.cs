using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using WhisperBranch.Persistence.Context;

namespace WhisperBranch.Persistence.ContextFactory
{
    public class WBContextFactory: IDesignTimeDbContextFactory<WBContext>
    {
        public WBContext CreateDbContext(string[] args)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true) // для локальной разработки
                .AddJsonFile($"appsettings.{environment}.json", optional: true)
                .AddEnvironmentVariables(); // при запуске в docker или через shell

            var configuration = builder.Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            if (string.IsNullOrEmpty(connectionString))
                throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            var optionsBuilder = new DbContextOptionsBuilder<WBContext>();
            optionsBuilder.UseNpgsql(connectionString);

            return new WBContext(optionsBuilder.Options);
        }
    }
  
}
