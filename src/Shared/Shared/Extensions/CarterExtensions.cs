using Carter;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Shared.Extensions
{
    public static class CarterExtensions
    {
        public static IServiceCollection AddCarterWithAssemblies(this IServiceCollection service, params Assembly[] assemblies)
        {
            service.AddCarter(configurator: config =>
            {
                foreach(var assembly in assemblies)
                {
                    var modules = assembly.GetTypes()
                        .Where(t => t.IsAssignableTo(typeof(ICarterModule)))
                        .ToArray();
                    config.WithModules(modules);
                } 
            });
            return service;
        }
    }
}
