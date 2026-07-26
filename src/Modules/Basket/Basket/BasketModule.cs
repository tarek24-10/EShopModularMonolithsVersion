using Basket.Data.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Data;
using Shared.Data.Interceptors;

namespace Basket
{
    public static class BasketModule
    {
        public static IServiceCollection AddBasketModule(this IServiceCollection service,
            IConfiguration configuration)
        {
            service.AddScoped<IBasketRepository, BasketRepository>();

            service.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
            service.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();
            service.AddDbContext<BasketDbContext>((sp, options) => {
                options.UseNpgsql(configuration.GetConnectionString("Database"));
                options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
            });

            return service;
        }

        public static IApplicationBuilder UseBasketModule(this IApplicationBuilder app)
        {
            app.UseMigration<BasketDbContext>();
            return app;
        }
    }
}
