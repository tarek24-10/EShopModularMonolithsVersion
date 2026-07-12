using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Data;

namespace Catalog
{
    public static class CatalogModule
    {
        public static IServiceCollection AddCatalogModule(this IServiceCollection service, 
            IConfiguration configuration)
        {
            service.AddDbContext<CatalogDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("Database")));
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
