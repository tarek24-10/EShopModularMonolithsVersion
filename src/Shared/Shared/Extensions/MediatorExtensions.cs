using FluentValidation;
using Shared.Behaviours;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Shared.Extensions
{
    public static class MediatorExtensions
    {
        public static IServiceCollection AddMediatorWithAssemblies(this IServiceCollection service, params Assembly[] assemblies)
        {
            service.AddMediatR(config =>
            {
                config.RegisterServicesFromAssemblies(assemblies);
                config.AddOpenBehavior(typeof(VaildationBehaviour<,>));
                config.AddOpenBehavior(typeof(LoggingBehaviour<,>));
            }
            );

            service.AddValidatorsFromAssemblies(assemblies);

            return service;
        }
    }
}
