using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Data.Interceptors;


namespace Catalog
{
    public static class CatalogModule
    {
        public static IServiceCollection AddCatalogModule(this IServiceCollection service, 
            IConfiguration configuration)
        {
            service.AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            service.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
            service.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();
            service.AddDbContext<CatalogDbContext>((sp, options) => {
                options.UseNpgsql(configuration.GetConnectionString("Database"));
                options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
            });

            service.AddScoped<IDataSeeder, CatalogDataSeeder>();
            return service;
        }

        public static IApplicationBuilder UseCatalogModule(this IApplicationBuilder app)
        {
            //InitializeDatabaseAsync(app).GetAwaiter().GetResult();

            app.UseMigration<CatalogDbContext>(); 
            return app;
        }

        //private static async Task InitializeDatabaseAsync(IApplicationBuilder app)
        //{
        //    using var scope = app.ApplicationServices.CreateScope();

        //    var context = scope.ServiceProvider.GetRequiredService<CatalogDbContext>();

        //    await context.Database.MigrateAsync();
        //}
    }
}
