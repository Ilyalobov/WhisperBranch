using WhisperBranch.Persistence.Context;
using WhisperBranch.Persistence.DependencyInjection;
using FluentValidation;
using FluentValidation.AspNetCore;
using WhisperBranch.Application.Graph;
using WhisperBranch.Application.Dispatcher.QueryHandling;
using WhisperBranch.Application.Dispatcher.CommandHandling;
using WhisperBranch.Application.Dispatcher.QueryHandling.Validation;
using WhisperBranch.Application.Dispatcher.CommandHandling.Validation;

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
            

            
            services.Scan(s => s
                   .FromAssemblyOf<IQueryDispatcher>() // Application
                   .AddClasses(c => c.AssignableTo(typeof(IQueryHandler<,>)))
                   .AsImplementedInterfaces()
                   .WithScopedLifetime()
                   .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<,>)))
                   .AsImplementedInterfaces()
                   .WithScopedLifetime());


            // ===== Регистрация валидаторов =====
            services.AddValidatorsFromAssemblyContaining<IQueryDispatcher>();
            services.AddValidatorsFromAssemblyContaining<ICommandDispatcher>();
            services.AddValidatorsFromAssemblyContaining<GraphValidator>();

            // ===== Регистрация диспетчеров =====
            services.AddScoped<IQueryDispatcher, QueryDispatcher>();
            services.AddScoped<ICommandDispatcher, CommandDispatcher>();

            // ===== РЕГИСТРАЦИЯ ДЕКОРАТОРОВ =====
            services.Decorate(typeof(IQueryHandler<,>), typeof(ValidationQueryHandlerDecorator<,>));

            services.Decorate(typeof(ICommandHandler<,>), typeof(ValidationCommandHandlerDecorator<,>));

            services.Decorate(typeof(ICommandHandler<,>), typeof(TransactionCommandHandlerDecorator<,>));

            services.Decorate(typeof(IQueryHandler<,>), typeof(TransactionQueryHandlerDecorator<,>));
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
