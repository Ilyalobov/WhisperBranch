using WhisperBranch.Persistence.Context;
using WhisperBranch.Persistence.DependencyInjection;
using FluentValidation;
using FluentValidation.AspNetCore;
using WhisperBranch.Application.Graph;

namespace WhisperBranch.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen();
            services.AddPersistence(Configuration);
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining<GraphValidator>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.ApplyMigrations<WBContext>();
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "WhisperBranch API V1");
                    c.RoutePrefix = string.Empty; 
                });
                app.UseDeveloperExceptionPage();

            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

           
            
        }
    }
 
}
