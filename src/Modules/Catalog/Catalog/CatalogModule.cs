using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog
{
    public static class CatalogModule
    {
        public static IServiceCollection AddCatallogModule(this IServiceCollection service, 
            IConfiguration configuration)
        {
            return service;
        }
    }
}
