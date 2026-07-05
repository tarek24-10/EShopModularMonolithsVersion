using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;

namespace Ordering
{
    public static class OrderingModule
    {
        public static IServiceCollection AddOrderingModule(this IServiceCollection service,
            IConfiguration configuration)
        {
            return service;
        }

        public static IApplicationBuilder UseOrderingModule(this IApplicationBuilder app)
        {
            return app;
        }
    }
}
