using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WhisperBranch.Persistence.Context;

namespace WhisperBranch.Persistence.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<WBContext>(options =>
                options.UseNpgsql(config.GetConnectionString("DefaultConnection")));

           
            return services;
        }
    }
}
